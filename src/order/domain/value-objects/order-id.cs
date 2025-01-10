using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderId : IValueObject<OrderId>
    {
        private readonly string _id;

        public OrderId(string id)
        {
            if (!UUIDValidator.IsValid(id))
            {
                throw new InvalidOrderIdException();
            }
            _id = id;
        }

        public string GetId()
        {
            return _id;
        }

        public bool Equals(OrderId other)
        {
            return other.GetId() == _id;
        }
    }
}
