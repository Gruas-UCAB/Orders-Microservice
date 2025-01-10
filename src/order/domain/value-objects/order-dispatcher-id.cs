using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderDispatcherId : IValueObject<OrderDispatcherId>
    {
        private readonly string _id;
        public OrderDispatcherId(string id)
        {
            if (!UUIDValidator.IsValid(id))
            {
                throw new InvalidOrderDispatcherIdException();
            }
            _id = id;
        }
        public string GetId()
        {
            return _id;
        }
        public bool Equals(OrderDispatcherId other)
        {
            return _id == other.GetId();
        }
    }
}
