
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
        private  bool _isActive = true;
        private Vehicle _vehicle;
        private Policy _policy;

        protected override void ValidateState()
        {
            if (_contractNumber == null   || _contractExpirationDate == null || _vehicle == null || _policy == null) 
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

     
        public static Contract Create(ContractId id, NumberContract numberContract, ContractExpitionDate expirationDate, Vehicle vehicle/*, Policy policy*/) 
        {
            Contract contract = new(id);
            contract.Apply(ContractCreated.CreateEvent(id, numberContract, expirationDate, vehicle /*, policy*/));
            return contract;
           
        }

        public Vehicle  AddVehicle(VehicleId id, VehicleLicensePlate vehicleLicensePlate,
               VehicleBrand vehicleBrand, VehicleModel vehicleModel, VehicleYear vehicleYear,
               VehicleColor vehicleColor, VehicleKm vehicleKm)
        {
            Apply(VehicleCreated.CreateEvent(_id, id, vehicleLicensePlate, vehicleBrand, vehicleModel, vehicleYear, vehicleColor, vehicleKm));
            return _vehicle;
        }
        public Policy AddPolicy(PolicyId id, PolicyName name, PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage kmCoverage)
        {
            Apply(PolicyCreated.CreateEvent(_id, id,name, monetaryCoverage, kmCoverage));
            return _policy;
        }

        private void OnContractCreatedEvent(ContractCreated Event)
        {
            _contractNumber = new NumberContract(Event.NumberContract);
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
            _vehicle = Event.Vehicle;
            /*_policy = Event.Policy;*/
        }
        /*
        private Vehicle OnVehicleCreatedEvent(VehicleCreated Event)
        {
            var vehicle = new Vehicle(
                    new VehicleId(Event.Id),
                    new VehicleLicensePlate(Event.LicensePlate),
                    new VehicleBrand(Event.Brand),
                    new VehicleModel(Event.Model),
                    new VehicleYear(Event.Year),
                    new VehicleColor(Event.Color),
                    new VehicleKm(Event.Km)
                    );
            _vehicle.Add(vehicle);// este no ?
            return vehicle;
        }*/
        /*
        private Policy OnPolicyCreatedEvent(PolicyCreated Event)
        {
            var policy = new Policy(
                    new PolicyId(Event.Id),
                    new PolicyName(Event.Name),
                    new PolicyMonetaryCoverage(Event.MonetaryCoverage),
                    new PolicyKmCoverage(Event.KmCoverage)
                );
            _policy.Add(policy);
            return policy;
        }
        */




        public void UpdateNumberContract(NumberContract numberContract)
        {
            Apply(NumberContractUpdated.CreateEvent(_id, numberContract));
            Console.WriteLine("Ya aplico");
        }

        public void UpdateExpirationDate(ContractExpitionDate expirationDate)
        {
            Apply(ExpirationDateUpdated.CreateEvent(_id, expirationDate));
        }

        private void OnContractNumberUpdatedEvent(NumberContractUpdatedEvent Event)
        {
            Console.WriteLine("Ya reacciono");
             _contractNumber = new NumberContract(Event.NumberContract);
        }

        private void OnContractExpirationDateUpdatedEvent(ExpirationDateUpdated Event)
        {
            _contractExpirationDate = new ContractExpitionDate(Event.ExpirationDate);
        }
        
    }
}
