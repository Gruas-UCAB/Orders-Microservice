using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyIdException : DomainException
    {
        public InvalidPolicyIdException() : base("Invalid Policy ID")
        {
        }
    }
}
