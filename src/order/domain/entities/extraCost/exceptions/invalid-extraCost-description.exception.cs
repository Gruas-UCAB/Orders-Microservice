using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.order.domain.entities.extraCost.exceptions
{
    public class InvalidExtraCostDescriptionException : DomainException
    {
        public InvalidExtraCostDescriptionException() : base("Invalid extra cost description")
        {
        }
    }
}
