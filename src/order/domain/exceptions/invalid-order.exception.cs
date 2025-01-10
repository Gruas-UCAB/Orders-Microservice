using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderException : DomainException
    {
        public InvalidOrderException() : base("Invalid order")
        {
        }
    }
}
