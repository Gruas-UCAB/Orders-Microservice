using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.services
{
    public record CalculateOrderCostWithExtraCostsDto(List<ExtraCost> ExtraCosts, Order Order, Contract Contract);
    public class CalculateOrderCostWithExtraCostsService : IDomainService<CalculateOrderCostWithExtraCostsDto, OrderCost>
    {
        public OrderCost Execute(CalculateOrderCostWithExtraCostsDto data)
        {
            var extraCostsTotal = data.ExtraCosts.Sum(e => e.GetPrice());
            var policy = data.Contract.GetPolicy();
            if (extraCostsTotal <= policy.GetMonetaryCoverage() && data.Order.IsCostCoveredByPolicy())
            {
                data.Order.CostCoveredByPolicy();
                return new OrderCost(data.Order.GetCost());
            }
            data.Order.CostNotCoveredByPolicy();
            var cost = data.Order.GetCost() + extraCostsTotal - policy.GetMonetaryCoverage();
            return (cost < 0) ? new OrderCost(cost * -1) : new OrderCost(cost);
        }
    }
}
