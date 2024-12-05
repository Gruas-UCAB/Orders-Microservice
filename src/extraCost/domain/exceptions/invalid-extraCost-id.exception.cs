using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.extracost.domain.exceptions
{
    public class InvalidExtraCostIdException : DomainException
{
    public InvalidExtraCostIdException() : base("Invalid extra cost ID")
    {
    }
}
}
