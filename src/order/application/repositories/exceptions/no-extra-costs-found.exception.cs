namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class NoExtraCostFoundException : ApplicationException
    {
        public NoExtraCostFoundException() : base("No extra costs found")
        {
        }
    }
}
