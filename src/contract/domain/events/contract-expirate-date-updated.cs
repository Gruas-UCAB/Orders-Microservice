
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractExpirationDateUpdatedEvent : DomainEvent<object>
        {   
        public ContractExpirationDateUpdatedEvent(string dispatcherId, string name, ContractExpirationDateUpdated context) : base(dispatcherId, name, context){ }
    }

    public class ContractExpirationDateUpdated( DateTime expirationDate ) 
    {
        public DateTime ExpirationDate = expirationDate;
        static public ContractExpirationDateUpdatedEvent CreateEvent(ContractId dispatcherId, ContractExpitionDate expirationDate )
        {
            return new ContractExpirationDateUpdatedEvent(
                dispatcherId.GetId(),
                typeof(ContractExpirationDateUpdated).Name,
                new ContractExpirationDateUpdated(
                    expirationDate.GetExpirationDateContract()  
                )
            );
        }
    }
}


