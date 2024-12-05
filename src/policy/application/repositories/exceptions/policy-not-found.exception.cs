namespace OrdersMicroservice.src.policy.application.repositories.exceptions
{
    public class PolicyNotFoundException : Exception
    {
        public PolicyNotFoundException() : base("Policy not found") { }
    }
}
