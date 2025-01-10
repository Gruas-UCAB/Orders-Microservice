using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class InvalidOrderCostException : DomainException
    {
        public InvalidOrderCostException() : base("Invalid order cost.")
        {
        }
    }
}
