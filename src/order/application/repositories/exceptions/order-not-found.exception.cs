namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class OrderNotFoundExcepion : ApplicationException
    {
        public OrderNotFoundExcepion() : base("Order not found") { }
    }
}
