using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.value_objects;

namespace OrdersMicroservice.src.policy.domain.events
{
    public class PolicyMonetaryCoverageUpdatedEvent : DomainEvent<object>
    {
        public PolicyMonetaryCoverageUpdatedEvent(string dispatcherId, string name, PolicyMonetaryCoverageUpdated context) : base(dispatcherId, name, context) { }
    }

    public class PolicyMonetaryCoverageUpdated(string monetaryCoverage)
    {
        public string MonetaryCoverage = monetaryCoverage;
        static public PolicyMonetaryCoverageUpdatedEvent CreateEvent(PolicyId dispatcherId, PolicyMonetaryCoverage monetaryCoverage)
        {
            Console.WriteLine("No aplico");
            return new PolicyMonetaryCoverageUpdatedEvent(
                dispatcherId.GetId(),
                typeof(PolicyMonetaryCoverageUpdated).Name,
                new PolicyMonetaryCoverageUpdated(
                    monetaryCoverage.GetMonetaryCoverage()
                )
            );
        }
    }
}
