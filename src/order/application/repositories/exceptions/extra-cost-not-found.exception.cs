namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class ExtraCostNotFoundException : ApplicationException
    {
        public ExtraCostNotFoundException() : base("Extra cost not found")
        {
        }
    }
}
