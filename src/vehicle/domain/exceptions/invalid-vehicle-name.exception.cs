using UsersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.vehicle.domain.exceptions
{
    public class InvalidVehicleNameException : DomainException
    {
        public InvalidVehicleNameException() : base("Invalid vehicle name")
        {
        }
    }
}
