using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyKmCoverageException : DomainException
    {
        public InvalidPolicyKmCoverageException() : base("Invalid km coverage")
        {
        }
    }
}
