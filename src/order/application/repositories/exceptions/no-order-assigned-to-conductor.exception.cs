namespace OrdersMicroservice.src.order.application.repositories.exceptions
{
    public class NoOrderAssignedToConductorException : ApplicationException
    {
        public NoOrderAssignedToConductorException() : base("No order assigned to conductor.") { }
    }
}
