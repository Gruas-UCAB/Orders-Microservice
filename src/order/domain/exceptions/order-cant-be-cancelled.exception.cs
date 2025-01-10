using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderCantBeCancelledException : DomainException
    {
        public OrderCantBeCancelledException() : base("Order can't be cancelled") { }
    }
}
