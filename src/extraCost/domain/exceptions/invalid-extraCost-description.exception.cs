using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.extracost.domain.exceptions
{
    public class InvalidExtraCostDescriptionException : DomainException
{
    public InvalidExtraCostDescriptionException() : base("Invalid extra cost description")
    {
    }
}
}
