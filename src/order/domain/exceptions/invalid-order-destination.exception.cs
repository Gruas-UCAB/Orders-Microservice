using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderDestinationException : DomainException
    {
        public InvalidOrderDestinationException() : base("Invalid order destination")
        {
        }
    }
}
