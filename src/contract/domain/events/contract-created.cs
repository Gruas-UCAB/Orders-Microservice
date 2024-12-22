
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.vehicle.domain.value_objects;

namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractCreatedEvent : DomainEvent<object>
        {   
        public ContractCreatedEvent(string dispatcherId, string name, ContractCreated context) : base(dispatcherId, name, context){ }
    }

    public class ContractCreated(decimal numberContract , DateTime expirationDate ,string vehicleId , string policy) 
    {
        public decimal NumberContract = numberContract;

        public DateTime ExpirationDate = expirationDate;

        public string VehicleId = vehicleId;

        public string  PolicyId = policy;


        static public ContractCreatedEvent CreateEvent(ContractId dispatcherId, NumberContract numberContract, ContractExpitionDate expirationDate , VehicleId vehicleId , PolicyId policyId)
        {
            return new ContractCreatedEvent(
                dispatcherId.GetContractId(),
                typeof(ContractCreated).Name,
                new ContractCreated(
                    numberContract.GetNumberContract(),
                    expirationDate.GetExpirationDateContract(),
                    vehicleId.GetId(),// tener vehicle
                    policyId.GetId()


                )
            );
        }
    }
}


