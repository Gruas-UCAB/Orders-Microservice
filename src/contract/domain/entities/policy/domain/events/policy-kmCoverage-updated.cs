using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;


namespace OrdersMicroservice.src.contract.domain.entities.policy.domain.events
{
    public class PolicykmCoverageUpdatedEvent : DomainEvent<object>
    {
        public PolicykmCoverageUpdatedEvent(string dispatcherId, string name, PolicykmCoverageUpdated context) : base(dispatcherId, name, context) { }
    }

    public class PolicykmCoverageUpdated(decimal kmCoverage)
    {
        public decimal KmCoverage = kmCoverage;
        static public PolicykmCoverageUpdatedEvent CreateEvent(PolicyId dispatcherId, PolicyKmCoverage kmCoverage)
        {
            Console.WriteLine("No aplico");
            return new PolicykmCoverageUpdatedEvent(
                dispatcherId.GetId(),
                typeof(PolicykmCoverageUpdated).Name,
                new PolicykmCoverageUpdated(
                    kmCoverage.GetKmCoverage()
                )
            );
        }
    }
}
