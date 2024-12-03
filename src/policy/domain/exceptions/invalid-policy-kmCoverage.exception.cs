using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.policy.domain.exceptions
{
    public class InvalidPolicyKmCoverageException : DomainException
{
    public InvalidPolicyKmCoverageException() : base("Invalid km coverage")
    {
    }
}
}
