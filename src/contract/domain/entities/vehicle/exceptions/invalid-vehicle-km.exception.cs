using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleKmException : DomainException
    {
        public InvalidVehicleKmException() : base("Invalid vehicle km.")
        {
        }
    }
}
