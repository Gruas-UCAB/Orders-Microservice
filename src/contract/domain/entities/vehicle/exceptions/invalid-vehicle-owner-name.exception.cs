using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleOwnerNameException : DomainException
    {
        public InvalidVehicleOwnerNameException() : base("Invalid vehicle owner name.")
        {
        }
    }
}
