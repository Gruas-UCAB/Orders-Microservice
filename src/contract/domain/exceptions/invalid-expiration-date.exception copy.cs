using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.exceptions
{
    public class InvalidExpirationDateException : DomainException
{
    public InvalidExpirationDateException() : base("Invalid date")
    {
    }
}
}
