using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidConductorAssignedIdException : DomainException
    {
        public InvalidConductorAssignedIdException() : base("Invalid conductor assigned id.")
        {
        }
    }
}
