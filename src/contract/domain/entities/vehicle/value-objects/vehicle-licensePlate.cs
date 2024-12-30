using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleLicensePlate : IValueObject<VehicleLicensePlate>
    {
        private readonly string _licensePlate;

        public VehicleLicensePlate(string licensePlate)
        {
            if (licensePlate.Length < 6 || licensePlate.Length > 7)
            {
                throw new InvalidLicensePlateException();
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(licensePlate, @"^[A-Z0-9\- ]+$"))
            {
                throw new InvalidLicensePlateException();
            }

            this._licensePlate = licensePlate;
        }

        public string GetLicensePlate()
        {
            return this._licensePlate;
        }

        public bool Equals(VehicleLicensePlate other)
        {
            return _licensePlate == other.GetLicensePlate();
        }
    }
