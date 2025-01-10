using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderNotAcceptedException : DomainException
    {
        public OrderNotAcceptedException() : base("Order not accepted")
        {
        }
    }
}
