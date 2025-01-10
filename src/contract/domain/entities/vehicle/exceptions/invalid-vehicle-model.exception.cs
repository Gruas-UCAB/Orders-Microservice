using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleModelException : DomainException 
    {
        public InvalidVehicleModelException() : base("Invalid vehicle model.")
        {
        }
    }
}
