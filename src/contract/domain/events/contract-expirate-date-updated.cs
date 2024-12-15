
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class ExpirationDateUpdatedEvent : DomainEvent<object>
        {   
        public ExpirationDateUpdatedEvent(string dispatcherId, string name, ExpirationDateUpdated context) : base(dispatcherId, name, context){ }
    }

    public class ExpirationDateUpdated( DateTime expirationDate ) 
    {
       

        public DateTime ExpirationDate = expirationDate;

     


        static public ExpirationDateUpdatedEvent CreateEvent(ContractId dispatcherId, ContractExpitionDate expirationDate )
        {
            return new ExpirationDateUpdatedEvent(
                dispatcherId.GetContractId(),
                typeof(ExpirationDateUpdated).Name,
                new ExpirationDateUpdated(
                    expirationDate.GetExpirationDateContract()  
                


                )
            );
        }
    }
}


