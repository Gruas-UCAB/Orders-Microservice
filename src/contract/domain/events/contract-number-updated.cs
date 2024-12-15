
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class NumberContractUpdatedEvent : DomainEvent<object>
        {
        internal decimal NumberContract;

        public NumberContractUpdatedEvent(string dispatcherId, string name, NumberContractUpdated context) : base(dispatcherId, name, context){ }
    }

    public class NumberContractUpdated(decimal numberContract ) 
    {
       

        public decimal NumberContract = numberContract;

     


        static public NumberContractUpdatedEvent CreateEvent(ContractId dispatcherId, NumberContract numberContract )
        {
            return new NumberContractUpdatedEvent(
                dispatcherId.GetContractId(),
                typeof(NumberContractUpdated).Name,
                new NumberContractUpdated(
                    numberContract.GetNumberContract() 
                


                )
            );
        }
    }
}


