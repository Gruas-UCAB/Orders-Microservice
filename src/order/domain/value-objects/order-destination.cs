using OrdersMicroservice.core.Common;
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderDestination : IValueObject<OrderDestination>
    {
        private readonly string _destination;

        public OrderDestination(string destination)
        {   
            if (!LocationValidator.IsValid(destination))
            {
                throw new InvalidOrderDestinationException();
            }
            _destination = destination;
        }
        public string GetDestination()
        {
            return _destination;
        }
        public bool Equals(OrderDestination other)
        {
            return _destination == other.GetDestination();
        }
    }
}
