using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyMonetaryCoverageException : DomainException
    {
        public InvalidPolicyMonetaryCoverageException() : base("Invalid monetary coverage")
        {
        }
    }
}
