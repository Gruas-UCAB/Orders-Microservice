namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class NoOrdersFoundException : ApplicationException
    {
        public NoOrdersFoundException() : base("No orders found")
        {
        }
    }
}
