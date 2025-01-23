using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.assign_conductor.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.services;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.assign_conductor
{
    public class AssignConductorCommandHandler(IOrderRepository orderRepository, IContractRepository contractRepository, IPublishEndpoint publishEndpoint) : IApplicationService<AssignConductorCommand, AssignConductorResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<AssignConductorResponse>> Execute(AssignConductorCommand data)
        {
            try
            {
                var conductorHasOrder = await _orderRepository.GetCurrentOrderByConductorId(new ConductorAssignedId(data.ConductorId));
                if (conductorHasOrder.HasValue())
                    return Result<AssignConductorResponse>.Failure(new ConductorAlreadyHasOrderAssignedException());
                var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
                if (!orderFind.HasValue())
                    return Result<AssignConductorResponse>.Failure(new OrderNotFoundExcepion());
                var order = orderFind.Unwrap();
                if (!string.Equals(order.GetStatus(), "por asignar", StringComparison.OrdinalIgnoreCase))
                    return Result<AssignConductorResponse>.Failure(new OrderCantBeAssignedException());
                order.AssignConductor(new ConductorAssignedId(data.ConductorId));
                order.SetStatus(new OrderStatus("por aceptar"));
                var contract = await _contractRepository.GetContractById(new ContractId(order.GetContractId()));
                if (!contract.HasValue())
                    return Result<AssignConductorResponse>.Failure(new ContractNotFoundException());
                var orderCostService = new CalculateOrderCostService();
                var orderCost = orderCostService.Execute(new CalculateOrderCostDto(order, data.TotalDistance, contract.Unwrap()));
                order.SetCost(orderCost);
                await _orderRepository.UpdateOrder(order);
                await _publishEndpoint.Publish(new OrderAssignedToConductor(Guid.Parse(order.GetId())));
                return Result<AssignConductorResponse>.Success(new AssignConductorResponse(order.GetId()));
            }
            catch (Exception e)
            {
                return Result<AssignConductorResponse>.Failure(e);
            }
        }
    }
}
