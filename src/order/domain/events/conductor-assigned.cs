using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.events
{
    public class ConductorAssignedEvent (string dispatcherId, string name, ConductorAssigned context) : DomainEvent<object>(dispatcherId, name, context) { }

    public class ConductorAssigned(string conductorId)
    {
        public string ConductorId = conductorId;
        public static ConductorAssignedEvent CreateEvent(OrderId dispatcherId, ConductorAssignedId conductorId)
        {
            return new ConductorAssignedEvent(
                dispatcherId.GetId(),
                typeof(ConductorAssigned).Name,
                new ConductorAssigned(
                    conductorId.GetId()
                )
            );
        }
    }
}
