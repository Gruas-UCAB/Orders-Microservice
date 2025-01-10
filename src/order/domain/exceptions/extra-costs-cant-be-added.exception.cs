using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class ExtraCostsCantBeAddedException : DomainException
    {
        public ExtraCostsCantBeAddedException() : base("Extra costs can't be added to order")
        {
        }
    }
}
