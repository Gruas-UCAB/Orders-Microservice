using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.policy.domain.exceptions
{
    public class InvalidPolicyIdException : DomainException
{
    public InvalidPolicyIdException() : base("Invalid Policy ID")
    {
    }
}
}
