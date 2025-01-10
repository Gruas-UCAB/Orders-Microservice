


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.entities.extraCost.exceptions;

namespace OrdersMicroservice.src.extracost.domain.value_objects;
public class ExtraCostPrice : IValueObject<ExtraCostPrice>
{
 private readonly decimal _extraCostPrice;
        public ExtraCostPrice(decimal extraCostPrice)
        {
            if (extraCostPrice < 0 )
            {
                throw new InvalidExtraCostPriceException();
            }
            this._extraCostPrice = extraCostPrice;
        }
        public decimal GetExtraCostPrice()
        {
            return this._extraCostPrice;
        }
        public bool Equals(ExtraCostPrice other)
        {
            return _extraCostPrice == other.GetExtraCostPrice();
        }
}
