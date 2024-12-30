using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;


namespace OrdersMicroservice.src.contract.domain.entities.policy.domain.events
{
    public class PolicyMonetaryCoverageUpdatedEvent : DomainEvent<object>
    {
        public PolicyMonetaryCoverageUpdatedEvent(string dispatcherId, string name, PolicyMonetaryCoverageUpdated context) : base(dispatcherId, name, context) { }
    }

    public class PolicyMonetaryCoverageUpdated(decimal monetaryCoverage)
    {
        public decimal MonetaryCoverage = monetaryCoverage;
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
