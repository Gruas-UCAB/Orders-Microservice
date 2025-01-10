using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderDispatcherIdException : DomainException
    {
        public InvalidOrderDispatcherIdException() : base("Invalid order dispatcher id.")
        {
        }
    }
}
