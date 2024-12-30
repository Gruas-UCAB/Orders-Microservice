using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyNameException : DomainException
    {
        public InvalidPolicyNameException() : base("Invalid policy name")
        {
        }
    }
}
