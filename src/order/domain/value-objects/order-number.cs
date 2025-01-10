using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderNumber : IValueObject<OrderNumber>
    {
        private readonly int _number;

        public OrderNumber(int number)
        {
            if (number <= 0)
            {
                throw new InvalidOrderNumberException();
            }
            _number = number;
        }

        public int GetNumber()
        {
            return _number;
        }
        public bool Equals(OrderNumber other)
        {
            return _number == other.GetNumber();
        }
    }
}
