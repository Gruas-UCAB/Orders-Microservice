using UsersMicroservice.Core.Domain;
using OrdersMicroservice.src.vehicle.domain.events;
using OrdersMicroservice.src.vehicle.domain.exceptions;
using OrdersMicroservice.src.vehicle.domain.value_objects;

namespace OrdersMicroservice.src.vehicle.domain
{
    public class Vehicle(VehicleId id) : AggregateRoot<VehicleId>(id)
    {
        private VehicleLicensePlate _licensePlate;
        private VehicleBrand _brand;
        private VehicleModel _model;
        private VehicleYear _year;
        private VehicleColor _color;
        private VehicleKm _km;

        protected override void ValidateState()
        {
            if (_licensePlate == null || _brand == null || _model == null || _year == null || _color == null || _km == null)
            {
                throw new InvalidVehicleException();
            }
        }

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
    }
}
