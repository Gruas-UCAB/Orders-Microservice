using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderCantBeAssignedException : DomainException
    {
        public OrderCantBeAssignedException() : base("Order can't be assigned")
        {
        }
    }
}
