using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.order.domain.entities.extraCost.exceptions
{
    public class InvalidExtraCostPriceException : DomainException
    {
        public InvalidExtraCostPriceException() : base("Invalid extra cost price")
        {
        }
    }
}
