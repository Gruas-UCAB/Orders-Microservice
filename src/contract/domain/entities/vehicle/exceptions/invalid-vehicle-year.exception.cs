using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidVehicleYearException : DomainException
    {
        public InvalidVehicleYearException() : base("Invalid vehicle year.")
        {
        }
    }
}
