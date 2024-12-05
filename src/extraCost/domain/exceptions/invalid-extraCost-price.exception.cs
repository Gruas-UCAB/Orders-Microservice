using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.extracost.domain.exceptions
{
    public class InvalidExtraCostPriceException : DomainException
{
    public InvalidExtraCostPriceException() : base("Invalid extra cost price")
    {
    }
}
}
