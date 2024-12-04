using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.value_objects;

namespace OrdersMicroservice.src.policy.domain.events
{
    public class PolicykmCoverageUpdatedEvent : DomainEvent<object>
    {
        public PolicykmCoverageUpdatedEvent(string dispatcherId, string name, PolicykmCoverageUpdated context) : base(dispatcherId, name, context) { }
    }

    public class PolicykmCoverageUpdated(string kmCoverage)
    {
        public string KmCoverage = kmCoverage;
        static public PolicykmCoverageUpdatedEvent CreateEvent(PolicyId dispatcherId, PolicyKmCoverage monetaryCoverage)
        {
            Console.WriteLine("No aplico");
            return new PolicykmCoverageUpdatedEvent(
                dispatcherId.GetId(),
                typeof(PolicykmCoverageUpdated).Name,
                new PolicykmCoverageUpdated(
                    monetaryCoverage.GetKmCoverage()
                )
            );
        }
    }
}
