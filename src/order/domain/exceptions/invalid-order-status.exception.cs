using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderStatusException : DomainException
    {
        public InvalidOrderStatusException() : base("Invalid order status")
        {
        }
    }
}
