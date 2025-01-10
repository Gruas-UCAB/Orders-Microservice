using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderDateException : DomainException
    {
        public InvalidOrderDateException() : base("Invalid order date.")
        {
        }
    }
}
