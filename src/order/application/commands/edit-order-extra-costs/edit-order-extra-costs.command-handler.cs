using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order.types;
using OrdersMicroservice.src.order.application.commands.edit_order_extra_costs.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.services;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.edit_order_extra_costs
{
    public class EditOrderExtraCostsCommandHandler(IOrderRepository orderRepository, IContractRepository contractRepository, IExtraCostRepository extraCostRepository) : IApplicationService<EditOrderExtraCostsCommand, EditOrderExtraCostsResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        public async Task<Result<EditOrderExtraCostsResponse>> Execute(EditOrderExtraCostsCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFind.HasValue())
            {
                return Result<EditOrderExtraCostsResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "localizado", StringComparison.OrdinalIgnoreCase))
            {
                return Result<EditOrderExtraCostsResponse>.Failure(new ExtraCostsCantBeAddedException());
            }

            foreach (var extraCost in data.ExtraCosts)
            {
                var extraCostFind = await _extraCostRepository.GetExtraCostById(new ExtraCostId(extraCost.Id));
                if (!extraCostFind.HasValue())
                    return Result<EditOrderExtraCostsResponse>.Failure(new ExtraCostNotFoundException());
                if (!order.GetExtraCosts().Exists(e => e.GetId() == extraCost.Id))
                    return Result<EditOrderExtraCostsResponse>.Failure(new ExtraCostIsNotPresentInOrderException());
                order.GetExtraCosts().ForEach(e =>
                {
                    if (e.GetId() == extraCost.Id)
                        e.SetPrice(new ExtraCostPrice(extraCost.Price));
                });
            }
            var service = new CalculateOrderCostWithExtraCostsService();
            var contract = await _contractRepository.GetContractById(new ContractId(order.GetContractId()));
            if (!contract.HasValue())
                return Result<EditOrderExtraCostsResponse>.Failure(new ContractNotFoundException());
            var orderCost = service.Execute(new CalculateOrderCostWithExtraCostsDto(order.GetExtraCosts(), order, contract.Unwrap()));
            order.SetCost(orderCost);
            await _orderRepository.UpdateOrder(order);
            return Result<EditOrderExtraCostsResponse>.Success(new EditOrderExtraCostsResponse(order.GetId()));
        }
    }
}
