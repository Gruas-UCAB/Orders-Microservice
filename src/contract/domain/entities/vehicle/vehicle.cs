using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle
{
    public class Vehicle : Entity<VehicleId>
    {
        private VehicleLicensePlate _licensePlate;
        private VehicleBrand _brand;
        private VehicleModel _model;
        private VehicleYear _year;
        private VehicleColor _color;
        private VehicleKm _km;

        public Vehicle( VehicleId id, VehicleLicensePlate vehicleLicensePlate,
               VehicleBrand vehicleBrand,VehicleModel vehicleModel, VehicleYear vehicleYear,
               VehicleColor vehicleColor, VehicleKm vehicleKm) : base(id)
        {
            _licensePlate = vehicleLicensePlate;
            _brand = vehicleBrand;
            _model = vehicleModel;
            _year = vehicleYear;
            _color = vehicleColor;
            _km = vehicleKm;

        }
        /*
        protected override void ValidateState()
        {
            if (_licensePlate == null || _brand == null || _model == null || _year == null || _color == null || _km == null)
            {
                throw new InvalidVehicleException();
            }
        }
        */
        public string GetId()
        {
            return _id.GetId();
        }
        public string GetLicensePlate()
        {
            return _licensePlate.GetLicensePlate();
        }
        public string GetBrand()
        {
            return _brand.GetBrand();
        }
        public string GetModel()
        {
            return _model.GetModel();
        }
        public int GetYear()
        {
            return _year.GetYear();
        }
        public string GetColor()
        {
            return _color.GetColor();
        }
        public double GetKm()
        {
            return _km.GetKm();
        }
        /*
        public static Vehicle Create(VehicleId id, VehicleLicensePlate licensePlate, VehicleBrand brand, VehicleModel model, VehicleYear year, VehicleColor color, VehicleKm km)
        {
            Vehicle vehicle = new(id);
            vehicle.Apply(VehicleCreated.CreateEvent(id, licensePlate, brand, model, year, color, km));
            return vehicle;
        }

        private void OnVehicleCreatedEvent(VehicleCreated Event)
        {
            _licensePlate = new VehicleLicensePlate(Event.LicensePlate);
            _brand = new VehicleBrand(Event.Brand);
            _model = new VehicleModel(Event.Model);
            _year = new VehicleYear(Event.Year);
            _color = new VehicleColor(Event.Color);
            _km = new VehicleKm(Event.Km);
        }
        */
    }
}
