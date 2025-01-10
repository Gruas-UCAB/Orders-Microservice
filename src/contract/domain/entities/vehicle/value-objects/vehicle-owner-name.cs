using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects
{
    public class VehicleOwnerName : IValueObject<VehicleOwnerName>
    {
        private readonly string _name;

        public VehicleOwnerName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                throw new InvalidVehicleOwnerNameException();
            }
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
        public bool Equals(VehicleOwnerName other)
        {
            return _name == other._name;
        }
    }
}
