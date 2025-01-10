namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class OrderDispatcherNotFoundException : ApplicationException
    {
        public OrderDispatcherNotFoundException() : base("Order dispatcher not found") { }
    }
}
