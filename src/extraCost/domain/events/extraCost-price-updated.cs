using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.value_objects;

namespace OrdersMicroservice.src.extracost.domain.events
{
    public class ExtraCostPriceUpdatedEvent : DomainEvent<object>
    {
        public ExtraCostPriceUpdatedEvent(string dispatcherId, string name, ExtraCostPicreUpdated context) : base(dispatcherId, name, context) { }
    }

    public class ExtraCostPicreUpdated(decimal extraCostPrice)
    {
        public decimal ExtraCostPrice = extraCostPrice;
        static public ExtraCostPriceUpdatedEvent CreateEvent(ExtraCostId dispatcherId, ExtraCostPrice extraCostPrice)
        {
            Console.WriteLine("No aplico");
            return new ExtraCostPriceUpdatedEvent(
                dispatcherId.GetExtraCostId(),
                typeof(ExtraCostPicreUpdated).Name,
                new ExtraCostPicreUpdated(
                    extraCostPrice.GetExtraCostPrice()
                )
            );
        }
    }
}
