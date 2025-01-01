using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractPolicyUpdatedEvent : DomainEvent<object>
    {
        public ContractPolicyUpdatedEvent(string dispatcherId, string name, ContractPolicyUpdated context) : base(dispatcherId, name, context) { }
    }

    public class ContractPolicyUpdated(Policy policy)
    {
        public Policy Policy = policy;
        static public ContractPolicyUpdatedEvent CreateEvent(ContractId dispatcherId, Policy policy)
        {
            return new ContractPolicyUpdatedEvent(
                dispatcherId.GetId(),
                typeof(ContractPolicyUpdated).Name,
                new ContractPolicyUpdated(
                    policy
                )
            );
        }
    }
}
