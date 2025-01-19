using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.value_objects;


namespace OrdersMicroservice.src.order.domain.entities.extraCost
{
    public class ExtraCost(ExtraCostId id, ExtraCostDescription description, ExtraCostPrice price) : Entity<ExtraCostId>(id)
    {
        private ExtraCostPrice _price = price;
        private ExtraCostDescription _description = description;

        public string GetId()
        {
            return _id.GetExtraCostId();
        }


        public string GetDescription()
        {
            return _description.GetExtraCostDescription();
        }

        public decimal GetPrice()
        {
            return _price.GetExtraCostPrice();
        }

        public void SetPrice(ExtraCostPrice price)
        {
            _price = price;
        }

        public void SetDescription(ExtraCostDescription description)
        {
            _description = description;
        }
    }
}
