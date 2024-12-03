using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.policy.domain.exceptions
{
    public class InvalidPolicyMonetaryCoverageException : DomainException
{
    public InvalidPolicyMonetaryCoverageException() : base("Invalid monetary coverage")
    {
    }
}
}
