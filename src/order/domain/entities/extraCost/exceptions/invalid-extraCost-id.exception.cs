using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.order.domain.entities.extraCost.exceptions
{
    public class InvalidExtraCostIdException : DomainException
    {
        public InvalidExtraCostIdException() : base("Invalid extra cost ID")
        {
        }
    }
}
