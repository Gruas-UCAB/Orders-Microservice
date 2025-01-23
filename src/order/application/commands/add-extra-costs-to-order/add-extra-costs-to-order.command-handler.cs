using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.services;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order
{
    public class AddExtraCostsToOrderCommandHandler(IOrderRepository orderRepository, IContractRepository contractRepository, IExtraCostRepository extraCostRepository) : IApplicationService<AddExtraCostsToOrderCommand, AddExtraCostsToOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        public async Task<Result<AddExtraCostsToOrderResponse>> Execute(AddExtraCostsToOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFind.HasValue())
            {
                return Result<AddExtraCostsToOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "localizado", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(order.GetStatus(), "en proceso", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(order.GetStatus(), "finalizado", StringComparison.OrdinalIgnoreCase) )
            {
                return Result<AddExtraCostsToOrderResponse>.Failure(new ExtraCostsCantBeAddedException());
            }
            var extraCostsToAdd = new List<ExtraCost>();
            foreach (var extraCost in data.ExtraCosts)
            {
                var extraCostFind = await _extraCostRepository.GetExtraCostById(new ExtraCostId(extraCost.Id));
                if (extraCostFind == null)
                    return Result<AddExtraCostsToOrderResponse>.Failure(new ExtraCostNotFoundException());
                extraCostsToAdd.Add(new ExtraCost(
                    new ExtraCostId(extraCost.Id), 
                    new ExtraCostDescription(extraCost.Description), 
                    new ExtraCostPrice(extraCost.Price))
                    );
            }
            order.AddExtraCosts(extraCostsToAdd);
            var service = new CalculateOrderCostWithExtraCostsService();
            var contract = await _contractRepository.GetContractById(new ContractId(order.GetContractId()));
            if (!contract.HasValue())
                return Result<AddExtraCostsToOrderResponse>.Failure(new ContractNotFoundException());
            var orderCost = service.Execute(new CalculateOrderCostWithExtraCostsDto(extraCostsToAdd, order, contract.Unwrap()));
            order.SetCost(orderCost);
            await _orderRepository.UpdateOrder(order);
            return Result<AddExtraCostsToOrderResponse>.Success(new AddExtraCostsToOrderResponse(order.GetId()));
        }
    }
}
