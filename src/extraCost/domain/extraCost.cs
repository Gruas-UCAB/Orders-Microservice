using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.extracost.domain.events;
using OrdersMicroservice.src.extracost.domain.exceptions;





namespace OrdersMicroservice.src.extracost.domain
{
    public class ExtraCost(ExtraCostId id) : AggregateRoot<ExtraCostId>(id)
    {
        private ExtraCostPrice _price;
        private ExtraCostDescription _description;
        
        
        
        protected override void ValidateState()
        {
            if (_price == null || _description == null )
            {
                throw new InvalidExtraCostException();
            }
        }

        public string GetExtraCostId()
        {
            return _id.GetExtraCostId();
        }

    
        public  string GetExtraCostDescription()
        {
            return _description.GetExtraCostDescription();
        }
        
        public decimal GetExtraCostPrice()
        {
            return _price.GetExtraCostPrice();
        }

    
        public static ExtraCost Create(ExtraCostId id, ExtraCostDescription description, ExtraCostPrice price)
        {
            ExtraCost extraCost = new(id);
            extraCost.Apply( ExtraCostCreated.CreateEvent(id, price, description)   );
            return extraCost;
           
        }

        public void UpdateExtraCostPrice(ExtraCostPrice price)
        {
            Apply(ExtraCostPicreUpdated.CreateEvent(_id, price));
            Console.WriteLine("Ya aplico");
        }

        public void UpdateExtraCostDescription(ExtraCostDescription description)
        {
            Apply(ExtraCostDescriptionUpdated.CreateEvent(_id, description));
            Console.WriteLine("Ya aplico");
        }


    
        private void OnExtraCostCreatedEvent(ExtraCostCreated Event)
        {
            _price = new ExtraCostPrice(Event.ExtraCostPrice);
            _description = new ExtraCostDescription(Event.ExtraCostDescription);

            
        }

        private void OnExtraCostPicreUpdatedEvent(ExtraCostPicreUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
              _price = new ExtraCostPrice(Event.ExtraCostPrice);
        }

        private void OnExtraCostDescriptionUpdatedEvent( ExtraCostDescriptionUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _description = new ExtraCostDescription(Event.ExtraCostDescription);
        }
    }
}
