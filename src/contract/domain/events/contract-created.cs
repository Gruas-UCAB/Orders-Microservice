
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class ContractCreatedEvent(string dispatcherId, string name, ContractCreated context) : DomainEvent<object>(dispatcherId, name, context){ }

    public class ContractCreated(int numberContract, DateTime expirationDate, Vehicle vehicle, Policy policy)
    {
        public int NumberContract = numberContract;
        public DateTime ExpirationDate = expirationDate;
        public  Vehicle  Vehicle = vehicle;
        public Policy Policy = policy;
        public static ContractCreatedEvent CreateEvent(ContractId dispatcherId, NumberContract numberContract, ContractExpitionDate expirationDate , Vehicle vehicle, Policy policy)
        {
            return new ContractCreatedEvent(
                dispatcherId.GetId(),
                typeof(ContractCreated).Name,
                new ContractCreated(
                    numberContract.GetNumberContract(),
                    expirationDate.GetExpirationDateContract(),
                    vehicle,
                    policy
                )
            );
        }
    }
}


