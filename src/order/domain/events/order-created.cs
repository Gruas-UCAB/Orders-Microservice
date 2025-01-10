using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.events
{
    public class OrderCreatedEvent(string dispatcherId, string name, OrderCreated context) : DomainEvent<object>(dispatcherId, name, context) { }
    public class OrderCreated(int numberOrder, DateTime date, string status, 
        string incidentType, string destination, string location, 
        string dispatcherId, decimal cost, string contractId)
    {
        public int NumberOrder = numberOrder;
        public DateTime Date = date;
        public string Status = status;
        public string IncidentType = incidentType;
        public string Destination = destination;
        public string Location = location;
        public string OrderDispatcherId = dispatcherId;
        public decimal Cost = cost;
        public string ContractId = contractId;

        public static OrderCreatedEvent CreateEvent(OrderId dispatcherId, OrderNumber numberOrder, OrderDate date, OrderStatus status,
            IncidentType incidentType, OrderDestination destination, OrderLocation location, OrderDispatcherId orderDispatcherId,
            OrderCost cost, ContractId contractId)
        {
            return new OrderCreatedEvent(
                dispatcherId.GetId(),
                typeof(OrderCreated).Name,
                new OrderCreated(
                    numberOrder.GetNumber(),
                    date.GetDate(),
                    status.GetStatus(),
                    incidentType.GetIncidentType(),
                    destination.GetDestination(),
                    location.GetLocation(),
                    orderDispatcherId.GetId(),
                    cost.GetCost(),
                    contractId.GetId()
                )
            );
        }
    }
}
