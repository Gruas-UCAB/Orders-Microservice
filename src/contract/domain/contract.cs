
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.events;
using OrdersMicroservice.src.contract.domain.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;




namespace OrdersMicroservice.src.contract.domain
{
    public class Contract(ContractId  id): AggregateRoot<ContractId>(id)
    {

        private NumberContract _contractNumber ;
        private ContractExpitionDate _contractExpirationDate ;
        private Vehicle _vehicle;
        private Policy _policy;
        private  bool _isActive = true;

        protected override void ValidateState()
        {
            if (_contractNumber == null   || _contractExpirationDate == null || _vehicle == null || _policy == null) 
            {
                throw new InvalidContractException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }
        public int GetContractNumber()
        {
            return _contractNumber.GetNumberContract();
        }

        public Vehicle GetVehicle()
        {
            return _vehicle;
        }

        public Policy GetPolicy()
        {
            return _policy;
        }
        public DateTime GetContractExpirationDate()
        {
            return _contractExpirationDate.GetExpirationDateContract();
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }
        public static Contract Create(ContractId id, NumberContract numberContract, ContractExpitionDate expirationDate, Vehicle vehicle, Policy policy) 
        {
            Contract contract = new(id);
            contract.Apply(ContractCreated.CreateEvent(id, numberContract, expirationDate, vehicle, policy));
            return contract;
        }
        public ContractId UpdateExpirationDate(ContractExpitionDate expirationDate)
        {
            Apply(ContractExpirationDateUpdated.CreateEvent(_id, expirationDate));
            return _id;
        }
        public Policy UpdateContractPolicy(Policy policy)
        {
            Apply(ContractPolicyUpdated.CreateEvent(_id, policy));
            return _policy;
        }
        private void OnContractCreatedEvent(ContractCreated Event)
        {
            _contractNumber = new NumberContract(Event.NumberContract);
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
            _vehicle = Event.Vehicle;
            _policy = Event.Policy;
        }
        private void OnContractExpirationDateUpdatedEvent(ContractExpirationDateUpdated Event)
        {
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
        }
        private void OnContractPolicyUpdatedEvent(ContractPolicyUpdated Event)
        {
            _policy = Event.Policy;
        }
    }
}
