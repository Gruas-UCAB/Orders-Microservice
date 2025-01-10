using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.events
{
    public class OrderFinishedEvent(string dispatcherId, string name, OrderFinished context) : DomainEvent<object>(dispatcherId, name, context) { }

    public class OrderFinished(string status)
    {
        public string Status = status;
        public static OrderFinishedEvent CreateEvent(OrderId dispatcherId, OrderStatus status)
        {
            return new OrderFinishedEvent(
                dispatcherId.GetId(),
                typeof(OrderFinished).Name,
                new OrderFinished(status.GetStatus())
            );
        }
    }
}
