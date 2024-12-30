using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleIdException : DomainException
    {
        public InvalidVehicleIdException() : base("Invalid vehicle ID")
        {
        }
    }
}
