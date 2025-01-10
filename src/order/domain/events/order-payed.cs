using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.events
{
    public class OrderPayedEvent(string dispatcherId, string name, OrderPayed context) : DomainEvent<object>(dispatcherId, name, context) { }

    public class OrderPayed
    {
        public bool Payed = true;
        public static OrderPayedEvent CreateEvent(OrderId dispatcherId)
        {
            return new OrderPayedEvent(
                dispatcherId.GetId(),
                typeof(OrderPayed).Name,
                new OrderPayed()
            );
        }
    }
}
