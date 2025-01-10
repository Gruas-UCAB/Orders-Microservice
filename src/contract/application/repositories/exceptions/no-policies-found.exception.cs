namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class NoPoliciesFoundException : Exception
    {
        public NoPoliciesFoundException() : base("No policies found") { }
    }
}
