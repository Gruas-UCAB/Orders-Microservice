using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain.events
{
    public class ExtraCostsAssignedEvent(string dispatcherId, string name, ExtraCostsAssigned context) : DomainEvent<object>(dispatcherId, name, context) { }

    public class ExtraCostsAssigned(List<ExtraCost> extraCosts)
    {
        public List<ExtraCost> ExtraCosts = extraCosts;
        public static ExtraCostsAssignedEvent CreateEvent(OrderId dispatcherId ,List<ExtraCost> extraCosts)
        {
            return new ExtraCostsAssignedEvent(
                dispatcherId.GetId(),
                typeof(ExtraCostsAssigned).Name,
                new ExtraCostsAssigned(extraCosts)
            );
        }
    }
}
