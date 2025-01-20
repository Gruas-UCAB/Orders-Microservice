using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.services
{
    public record CalculateOrderCostDto(Order Order, decimal TotalDistance, Contract Contract);
    public class CalculateOrderCostService : IDomainService<CalculateOrderCostDto, OrderCost>
    {
        public OrderCost Execute(CalculateOrderCostDto data)
        {
            var policy = data.Contract.GetPolicy();
            if (data.TotalDistance <= policy.GetkmCoverage())
                {
                data.Order.CostCoveredByPolicy();
                return new OrderCost(0);
                }
            data.Order.CostNotCoveredByPolicy();
            var baseKmPrice = policy.GetBaseKmPrice();
            var monetaryCoverage = policy.GetMonetaryCoverage();
            var cost = data.TotalDistance * baseKmPrice - monetaryCoverage;
            return (cost < 0) ? new OrderCost(cost * -1) : new OrderCost(cost);
        }
    }
}
