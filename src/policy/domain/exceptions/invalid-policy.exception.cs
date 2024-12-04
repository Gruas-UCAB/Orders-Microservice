using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.policy.domain.exceptions
{
    public class InvalidPolicyException : DomainException
    {
        public InvalidPolicyException() : base("Invalid policy")
        {
        }
    }
}
