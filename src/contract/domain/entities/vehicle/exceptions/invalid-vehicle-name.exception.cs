using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleNameException : DomainException
    {
        public InvalidVehicleNameException() : base("Invalid vehicle name")
        {
        }
    }
}
