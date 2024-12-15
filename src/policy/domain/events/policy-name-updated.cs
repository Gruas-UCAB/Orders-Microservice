using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.value_objects;

namespace OrdersMicroservice.src.policy.domain.events
{
    public class PolicyNameUpdatedEvent : DomainEvent<object>
    {
        public PolicyNameUpdatedEvent(string dispatcherId, string name, PolicyNameUpdated context) : base(dispatcherId, name, context) { }
    }

    public class PolicyNameUpdated(string name)
    {
        public string Name = name;
        static public PolicyNameUpdatedEvent CreateEvent(PolicyId dispatcherId, PolicyName name)
        {
            Console.WriteLine("No aplico");
            return new PolicyNameUpdatedEvent(
                dispatcherId.GetId(),
                typeof(PolicyNameUpdated).Name,
                new PolicyNameUpdated(
                    name.GetName()
                )
            );
        }
    }
}
