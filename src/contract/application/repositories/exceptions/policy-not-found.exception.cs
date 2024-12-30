namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class PolicyNotFoundException : Exception
    {
        public PolicyNotFoundException() : base("Policy not found") { }
    }
}
