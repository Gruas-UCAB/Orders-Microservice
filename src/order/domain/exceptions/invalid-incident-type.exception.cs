using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidIncidentTypeException : DomainException
    {
        public InvalidIncidentTypeException() : base("Invalid incident type")
        {
        }
    }
}
