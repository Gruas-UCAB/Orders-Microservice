using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class OrderNotAssignendException : DomainException
    {
        public OrderNotAssignendException() : base("Order not assigned")
        {
        }
    }
}
