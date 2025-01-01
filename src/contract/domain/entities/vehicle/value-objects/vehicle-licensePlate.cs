using OrdersMicroservice.core.Common;
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleLicensePlate : IValueObject<VehicleLicensePlate>
    {
        private readonly string _licensePlate;

        public VehicleLicensePlate(string licensePlate)
        {
            if (!PlateValidator.IsValid(licensePlate))
            {
                throw new InvalidLicensePlateException();
            }
            _licensePlate = licensePlate;
        }

        public string GetLicensePlate()
        {
            return _licensePlate;
        }

        public bool Equals(VehicleLicensePlate other)
        {
            return _licensePlate == other.GetLicensePlate();
        }
    }
