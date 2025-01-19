using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class ExtraCostIsNotPresentInOrderException : DomainException
    {
        public ExtraCostIsNotPresentInOrderException() : base("Extra cost is not present in this order"){ }
    }
}
