namespace OrdersMicroservice.src.policy.application.repositories.exceptions
{
    public class NoPoliciesFoundException : Exception
    {
        public NoPoliciesFoundException() : base("No policies found") { }
    }
}
