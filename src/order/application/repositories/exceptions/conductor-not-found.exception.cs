namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class conductorNotFoundException : ApplicationException
    {
        public conductorNotFoundException() : base("Conductor not found") { }
    }
}
