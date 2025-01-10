using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderNumberException : DomainException
    {
        public InvalidOrderNumberException() : base("Invalid order number.")
        {
        }
    }
}
