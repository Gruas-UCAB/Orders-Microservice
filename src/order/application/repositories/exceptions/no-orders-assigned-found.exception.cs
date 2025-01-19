namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class NoOrdersAssignedFoundException : ApplicationException
    {
        public NoOrdersAssignedFoundException() : base("No orders assigned found")
        {
        }
    }
}
