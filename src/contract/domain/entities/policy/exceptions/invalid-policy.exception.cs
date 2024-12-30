using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyException : DomainException
    {
        public InvalidPolicyException() : base("Invalid policy")
        {
        }
    }
}
