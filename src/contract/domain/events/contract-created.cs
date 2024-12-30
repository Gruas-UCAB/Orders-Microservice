
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractCreatedEvent : DomainEvent<object>
        {   
        public ContractCreatedEvent(string dispatcherId, string name, ContractCreated context) : base(dispatcherId, name, context){ }
    }

    public class ContractCreated(decimal numberContract , DateTime expirationDate ,Vehicle vehicle /*, Policy policy*/) 
    {
        public decimal NumberContract = numberContract;

        public DateTime ExpirationDate = expirationDate;

        public  Vehicle  Vehicle = vehicle;

       /* public  Policy Policy = policy;*/


        static public ContractCreatedEvent CreateEvent(ContractId dispatcherId, NumberContract numberContract, ContractExpitionDate expirationDate , Vehicle vehicle/*, Policy policy*/)
        {
            return new ContractCreatedEvent(
                dispatcherId.GetContractId(),
                typeof(ContractCreated).Name,
                new ContractCreated(
                    numberContract.GetNumberContract(),
                    expirationDate.GetExpirationDateContract(),
                    vehicle
                    /*policy*/


                )
            );
        }
    }
}


