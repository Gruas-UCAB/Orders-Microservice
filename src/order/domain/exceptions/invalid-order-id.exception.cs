using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderIdException : DomainException
    {
        public InvalidOrderIdException() : base("Invalid order id")
        {
        }
    }
}
