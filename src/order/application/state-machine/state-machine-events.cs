using System.Text.Json.Serialization;

namespace OrdersMicroservice.src.order.application.state_machine.events
{
    public class OrderCreated
    {
        public Guid OrderId { get; }

        [JsonConstructor]
        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class OrderAssignedToConductor
    {
        public Guid OrderId { get; } 
        [JsonConstructor]
        public OrderAssignedToConductor(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class OrderFinished
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public OrderFinished(Guid orderId)
        {
            OrderId = orderId;
        }

    }
    public class OrderCancelled
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public OrderCancelled(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class OrderPayed
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public OrderPayed(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class ConductorAcceptedOrder
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public ConductorAcceptedOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class ConductorRejectedOrder
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public ConductorRejectedOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class OrderLocatedByConductor
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public OrderLocatedByConductor(Guid orderId)
        {
            OrderId = orderId;
        }
    }
    public class OrderStartedByConductor
    {
        public Guid OrderId { get; }
        [JsonConstructor]
        public OrderStartedByConductor(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
