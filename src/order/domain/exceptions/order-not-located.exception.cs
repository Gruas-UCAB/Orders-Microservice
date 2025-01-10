using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderNotLocatedException : DomainException
    {
        public OrderNotLocatedException() : base("Order not located")
        {
        }
    }
}
