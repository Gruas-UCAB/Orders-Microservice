using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.contract.domain.entities.policy.exceptions
{
    public class InvalidPolicyBaseKmPriceException : DomainException
    {
        public InvalidPolicyBaseKmPriceException() : base("Invalid base km price")
        {
        }
    }
}
