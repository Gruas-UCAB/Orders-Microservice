using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderNotStartedException : DomainException
    {
        public OrderNotStartedException() : base("Order not started")
        {
        }
    }
}
