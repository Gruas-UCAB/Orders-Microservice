using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleOwnerDniException : DomainException
    {
        public InvalidVehicleOwnerDniException() : base("Invalid vehicle owner DNI.")
        {
        }
    }
}
