using MassTransit;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.create_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.create_order
{
    public class CreateOrderCommandHandler(IIdGenerator<string> idGenerator, IOrderRepository orderRepository, IContractRepository contractRepository, IPublishEndpoint publishEndpoint) : IApplicationService<CreateOrderCommand, CreateOrderResponse> 
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<CreateOrderResponse>> Execute(CreateOrderCommand data)
        {
            try
            {
                var contractFound = await _contractRepository.GetContractById(new ContractId(data.ContractId));
                if (!contractFound.HasValue())
                {
                    throw new ContractNotFoundException();
                }
                int orderNumber = 1001;
                var orders = await _orderRepository.GetAllOrders(new GetAllOrdersDto());
                if (orders.HasValue())
                {
                    orderNumber = orders.Unwrap().Last().GetOrderNumber() + 1;                
                }
                var orderId = _idGenerator.GenerateId();
                var order = Order.Create(
                    new OrderId(orderId),
                    new OrderNumber(orderNumber),
                    new OrderDate(data.Date),
                    new OrderStatus("por asignar"),
                    new IncidentType(data.IncidentType),
                    new OrderDestination(data.Destination),
                    new OrderLocation(data.Location),
                    new OrderDispatcherId(data.OrderDispatcherId),
                    new OrderCost(0),
                    new ContractId(data.ContractId)
                    );
                await _orderRepository.SaveOrder(order);
                await _publishEndpoint.Publish(new OrderCreated(Guid.Parse( orderId )));
                return Result<CreateOrderResponse>.Success(new CreateOrderResponse(order.GetId()));
            }
            catch (Exception e)
            {
                return Result<CreateOrderResponse>.Failure(e);
            }
        }
    }
}
