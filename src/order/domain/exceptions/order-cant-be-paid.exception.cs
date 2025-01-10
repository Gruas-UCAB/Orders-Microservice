using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderCantBePaidException : DomainException
    {
        public OrderCantBePaidException() : base("Order can't be paid")
        {
        }
    }
}
