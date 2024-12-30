using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleException : DomainException
    {
        public InvalidVehicleException() : base("Invalid vehicle")
        {
        }
    }
}
