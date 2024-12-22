
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.events;
using OrdersMicroservice.src.contract.domain.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.policy.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain
{
    public class Contract(ContractId  id): AggregateRoot<ContractId>(id)
    {

        private NumberContract _contractNumber ;
      
        private ContractExpitionDate _contractExpirationDate ;
        private  bool _isActive = true;
        private Vehicle _vehicleId;

        private PolicyId _policyId;

        protected override void ValidateState()
        {
            if (_contractNumber == null   || _contractExpirationDate == null || _vehicleId == null || _policyId == null) 
            {
                throw new InvalidContractException();
            }
        }

        public string GetContractId()
        {
            return _id.GetContractId();
        }
        public decimal GetContractNumber()
        {
            return _contractNumber.GetNumberContract();
        }

        public string GetVehicleId()
        {
            return _VehicleId.GetId();// tener vehicle
        }

        public string GetPolicyId()
        {
            return _policyId.GetId();
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

     
        public static Contract Create(ContractId id, NumberContract numberContract, ContractExpitionDate expirationDate, VehicleId vehicle, PolicyId policy) 
        {
            Contract contract = new(id);
            contract.Apply(ContractCreated.CreateEvent(id, numberContract, expirationDate, vehicle, policy));
            return contract;
           
        }

        public void UpdateNumberContract(NumberContract numberContract)
        {
            Apply(NumberContractUpdated.CreateEvent(_id, numberContract));
            Console.WriteLine("Ya aplico");
        }

        public void UpdateExpirationDate(ContractExpitionDate expirationDate)
        {
            Apply(ExpirationDateUpdated.CreateEvent(_id, expirationDate));
        }

        private void OnUserCreatedEvent(ContractCreated Event)
        {
            _contractNumber = new NumberContract(Event.NumberContract);
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
            _vehicleId = new VehicleId(Event.VehicleId);
            _policyId = new PolicyId(Event.PolicyId);
        }

        private void OnContractNumberUpdatedEvent(NumberContractUpdatedEvent Event)
        {
            Console.WriteLine("Ya reacciono");
             _contractNumber = new NumberContract(Event.NumberContract);//ver aqui esta raro
        }

        private void OnContractExpirationDateUpdatedEvent(ExpirationDateUpdated Event)
        {
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
        }

    }
}
