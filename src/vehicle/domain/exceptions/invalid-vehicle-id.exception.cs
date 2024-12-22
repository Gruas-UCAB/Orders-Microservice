using UsersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.vehicle.domain.exceptions
{
    public class InvalidVehicleIdException : DomainException
    {
        public InvalidVehicleIdException() : base("Invalid vehicle ID")
        {
        }
    }
}
