using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.policy.domain.exceptions
{
    public class InvalidPolicyNameException : DomainException
{
    public InvalidPolicyNameException() : base("Invalid policy name")
    {
    }
}
}
