using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleBrandException : DomainException
    {
        public InvalidVehicleBrandException() : base("Invalid vehicle brand") { }
    }
}
