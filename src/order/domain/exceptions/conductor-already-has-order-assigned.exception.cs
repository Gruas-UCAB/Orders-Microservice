using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.order.domain.exceptions
{
    public class ConductorAlreadyHasOrderAssignedException: DomainException
    {
        public ConductorAlreadyHasOrderAssignedException() : base("Conducto already has an order assigned."){}
    }
}
