using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace OrdersMicroservice.src.contract.domain.events
{
    public class VehicleCreatedEvent : DomainEvent<object>
    {
        public VehicleCreatedEvent(string dispatcherId, string name, VehicleCreated context) : base(dispatcherId, name, context) { }
    }

    public class VehicleCreated(string Id,string LicensePlate, string Brand, string Model, int Year, string Color, double Km)
    {
        public string Id = Id;
        public string LicensePlate = LicensePlate;
        public string Brand = Brand;
        public string Model = Model;
        public int Year = Year;
        public string Color = Color;
        public double Km = Km;

        static public VehicleCreatedEvent CreateEvent(ContractId DispatcherId, VehicleId VehicleId, VehicleLicensePlate LicensePlate, VehicleBrand Brand, VehicleModel Model, VehicleYear Year, VehicleColor Color, VehicleKm Km)
        {
            return new VehicleCreatedEvent(
                DispatcherId.GetContractId(),
                typeof(VehicleCreated).Name,
                new VehicleCreated(
                    VehicleId.GetId(),
                    LicensePlate.GetLicensePlate(),
                    Brand.GetBrand(),
                    Model.GetModel(),
                    Year.GetYear(),
                    Color.GetColor(),
                    Km.GetKm()

                )
            );
        }
    }
}
