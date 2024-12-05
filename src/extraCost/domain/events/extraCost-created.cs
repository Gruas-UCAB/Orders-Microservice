using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.value_objects;
namespace OrdersMicroservice.src.extracost.domain.events
{
    public class ExtraCostCreatedEvent : DomainEvent<object>
        {   
        public ExtraCostCreatedEvent(string dispatcherId, string name, ExtraCostCreated context) : base(dispatcherId, name, context){ }
    }

    public class ExtraCostCreated( decimal extraCostPrice, string extraCostDescription)
    {
        
        public decimal ExtraCostPrice = extraCostPrice;
        public string ExtraCostDescription = extraCostDescription;
        
        static public ExtraCostCreatedEvent CreateEvent(ExtraCostId dispatcherId,  ExtraCostPrice extraCostPrice, ExtraCostDescription extraCostDescription)
        {
            return new ExtraCostCreatedEvent(
                dispatcherId.GetExtraCostId(),
                typeof(ExtraCostCreated).Name,
                new ExtraCostCreated(
                    extraCostPrice.GetExtraCostPrice(),
                    extraCostDescription.GetExtraCostDescription()

                )
            );
        }
    }
}



