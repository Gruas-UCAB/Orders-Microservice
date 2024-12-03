using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.exceptions
{
    public class InvalidPolicyException : DomainException
    {
        public InvalidPolicyException() : base("Invalid policy")
        {
        }
    }
}
