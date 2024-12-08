using UsersMicroservice.Core.Domain;
using OrdersMicroservice.src.vehicle.domain.value_objects;

namespace OrdersMicroservice.src.vehicle.domain.events
{
    public class VehicleCreatedEvent : DomainEvent<object>
    {
        public VehicleCreatedEvent(string dispatcherId, string name, VehicleCreated context) : base(dispatcherId, name, context) { }
    }

    public class VehicleCreated(string licensePlate, string brand, string model, int year, string color, double km)
    {
        public string LicensePlate = licensePlate;
        public string Brand = brand;
        public string Model = model;
        public int Year = year;
        public string Color = color;
        public double Km = km;

        static public VehicleCreatedEvent CreateEvent(VehicleId dispatcherId, VehicleLicensePlate licensePlate, VehicleBrand brand, VehicleModel model, VehicleYear year, VehicleColor color, VehicleKm km)
        {
            return new VehicleCreatedEvent(
                dispatcherId.GetId(),
                typeof(VehicleCreated).Name,
                new VehicleCreated(
                    licensePlate.GetLicensePlate(),
                    brand.GetBrand(),
                    model.GetModel(),
                    year.GetYear(),
                    color.GetColor(),
                    km.GetKm()
                )
            );
        }
    }
}
