using OrdersMicroservice.core.Common;
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderLocation : IValueObject<OrderLocation>
    {
        private readonly string _location;

        public OrderLocation(string location)
        {
            if (!LocationValidator.IsValid(location))
            {
                throw new InvalidOrderLocationException();
            }
            _location = location;
        }
        public string GetLocation()
        {
            return _location;
        }
            public bool Equals(OrderLocation other)
        {
            return _location == other.GetLocation();
        }
    }
}
