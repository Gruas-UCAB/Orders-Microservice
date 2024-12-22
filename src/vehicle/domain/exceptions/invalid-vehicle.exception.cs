

using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.vehicle.domain.exceptions
{
    public class InvalidVehicleException : DomainException
    {
        public InvalidVehicleException() : base("Invalid vehicle")
        {
        }
    }
}
