using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects
{
    public class VehicleOwnerDni : IValueObject<VehicleOwnerDni>
    {
        private readonly int _dni;

        public VehicleOwnerDni(int dni)
        {
            if (dni <= 4000000 || dni > 32000000)
            {
                throw new InvalidVehicleOwnerDniException();
            }
            _dni = dni;
        }
        public int GetDni()
        {
            return _dni;
        }
        public bool Equals(VehicleOwnerDni other)
        {
            throw new NotImplementedException();
        }
    }
}
