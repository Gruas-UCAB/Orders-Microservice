using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle
{
    public class Vehicle(VehicleId id, VehicleLicensePlate vehicleLicensePlate,
           VehicleBrand vehicleBrand, VehicleModel vehicleModel, VehicleYear vehicleYear,
           VehicleColor vehicleColor, VehicleKm vehicleKm, VehicleOwnerDni ownerDni, VehicleOwnerName ownerName) : Entity<VehicleId>(id)
    {
        private VehicleLicensePlate _licensePlate = vehicleLicensePlate;
        private VehicleBrand _brand = vehicleBrand;
        private VehicleModel _model = vehicleModel;
        private VehicleYear _year = vehicleYear;
        private VehicleColor _color = vehicleColor;
        private VehicleKm _km = vehicleKm;
        private VehicleOwnerDni _ownerDni = ownerDni;
        private VehicleOwnerName _ownerName = ownerName;

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
        public int GetKm()
        {
            return _km.GetKm();
        }
        public int GetOwnerDni()
        {
            return _ownerDni.GetDni();
        }
        public string GetOwnerName() {
            return _ownerName.GetName();
        }
    }
}
