using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace OrdersMicroservice.src.contract.domain.events
{
    public class PolicyCreatedEvent : DomainEvent<object>
    {
        public PolicyCreatedEvent(string dispatcherId, string name, PolicyCreated context) : base(dispatcherId, name, context) { }
    }

    public class PolicyCreated(string Id, string Name, decimal MonetaryCoverage, decimal KmCoverage)
    {
        public string Id = Id;
        public string Name = Name;
        public decimal MonetaryCoverage = MonetaryCoverage;
        public decimal KmCoverage = KmCoverage;
    
        static public PolicyCreatedEvent CreateEvent(ContractId DispatcherId, PolicyId PolicyId, PolicyName Name, PolicyMonetaryCoverage MonetaryCoverage, PolicyKmCoverage KmCoverage)
        {
            return new PolicyCreatedEvent(
                DispatcherId.GetContractId(),
                typeof(PolicyCreated).Name,
                new PolicyCreated(
                    PolicyId.GetId(),
                    Name.GetName(),
                    MonetaryCoverage.GetMonetaryCoverage(),
                    KmCoverage.GetKmCoverage()

                )
            );
        }
    }
}



