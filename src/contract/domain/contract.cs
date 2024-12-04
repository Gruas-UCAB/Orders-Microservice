/*
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.events;
using OrdersMicroservice.src.contract.domain.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using System.Xml.Linq;


namespace OrdersMicroservice.src.contract.domain
{
    public class Contract(ContractId  id): AggregateRoot<ContractId>(id)
    {
        private ContractId _id = id;
        private string _ContractNumber ;
        private VehicleId _EnsuredVehicleId ; //definir
        private PolicyId _Policy ;//definir
        private DateTime _ContractExpirationDate ;
        private  bool _IsActive = true;

        protected override void ValidateState()
        {
            if (_ContractNumber == null  || _EnsuredVehicleId == null || _Policy == null   )
            {
                throw new InvalidContractException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }
        public string GetContractNumber()
        {
            return _ContractNumber;
        }

        public string GetEnsuredVehicleId()
        {
            return _EnsuredVehicleId;
        }

        public string GetPolicyId()
        {
            return _Policy;
        }


        public DateTime GetContractExpirationDate()
        {
            return _ContractExpirationDate;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }

     
        public static Contract Create(UserId id, UserName name, UserPhone phone, UserRole role, DepartmentId department)
        {
            User user = new(id);
            user.Apply(UserCreated.CreateEvent(id, name, phone, role, department));
            return user;
        }

        public void UpdateName(UserName name)
        {
            Apply(UserNameUpdated.CreateEvent(_id, name));
            Console.WriteLine("Ya aplico");
        }

        public void UpdatePhone(UserPhone phone)
        {
            Apply(UserPhoneUpdated.CreateEvent(_id, phone));
        }

        private void OnUserCreatedEvent(UserCreated Event)
        {
            _name = new UserName(Event.Name);
            _phone = new UserPhone(Event.Phone);
            _role = new UserRole(Event.Role);
            _department = new DepartmentId(Event.Department);
        }

        private void OnUserNameUpdatedEvent(UserNameUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _name = new UserName(Event.Name);
        }

        private void OnUserPhoneUpdatedEvent(UserPhoneUpdated Event)
        {
            _phone = new UserPhone(Event.Phone);
        }

    }
}
*/