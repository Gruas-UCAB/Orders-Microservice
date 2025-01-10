using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderCost : IValueObject<OrderCost>
    {
        private readonly decimal _cost;

        public OrderCost(decimal cost)
        {
            if (cost < 0)
            {
                throw new InvalidOrderCostException();
            }
            _cost = cost;
        }

        public decimal GetCost()
        {
            return _cost;
        }
        public bool Equals(OrderCost other)
        {
            return _cost == other.GetCost();
        }
    }
}
