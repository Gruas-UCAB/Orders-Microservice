using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.value_objects;

namespace OrdersMicroservice.src.extracost.domain.events
{
    public class ExtraCostDescriptionUpdatedEvent : DomainEvent<object>
    {
        public ExtraCostDescriptionUpdatedEvent(string dispatcherId, string name, ExtraCostDescriptionUpdated context) : base(dispatcherId, name, context) { }
    }

    public class ExtraCostDescriptionUpdated(string extraCostDescription)
    {
        public string ExtraCostDescription = extraCostDescription;
        static public ExtraCostDescriptionUpdatedEvent CreateEvent(ExtraCostId dispatcherId, ExtraCostDescription extraCostDescription)
        {
            Console.WriteLine("No aplico");
            return new ExtraCostDescriptionUpdatedEvent(
                dispatcherId.GetExtraCostId(),
                typeof(ExtraCostDescriptionUpdated).Name,
                new ExtraCostDescriptionUpdated(
                    extraCostDescription.GetExtraCostDescription()
                )
            );
        }
    }
}
