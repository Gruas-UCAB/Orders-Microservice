using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderLocationException : DomainException
    {
        public InvalidOrderLocationException() : base("Invalid order location")
        {
        }
    }
}
