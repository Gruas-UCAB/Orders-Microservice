using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleColorException : DomainException
    {
        public InvalidVehicleColorException() : base("Invalid vehicle color")
        {
        }
    }
}
