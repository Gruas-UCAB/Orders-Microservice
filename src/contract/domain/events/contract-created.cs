/*
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractCreatedEvent : DomainEvent<object>
        {   
        public ContractCreatedEvent(string dispatcherId, string name, ContractCreated context) : base(dispatcherId, name, context){ }
    }

    public class ContractCreated(string contractNumber)
    {
        public string ContractNumber = contractNumber;
        static public ContractCreatedEvent CreateEvent(ContractId dispatcherId, ContractNumber contractNumber)
        {
            return new ContractCreatedEvent(
                dispatcherId.GetId(),
                typeof(ContractCreated).contractNumber,
                new ContractCreated(
                    name.GetName()
                )
            );
        }
    }
}

*/

