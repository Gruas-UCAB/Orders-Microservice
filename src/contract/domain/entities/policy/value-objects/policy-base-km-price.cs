using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.policy.value_objects
{
    public class PolicyBaseKmPrice : IValueObject<PolicyBaseKmPrice>
    {
        private readonly decimal _price;

        public PolicyBaseKmPrice(decimal price)
        {
            if (price < 0)
            {
                throw new InvalidPolicyBaseKmPriceException();
            }
            _price = price;
        }

        public decimal GetPrice()
        {
            return _price;
        }
        public bool Equals(PolicyBaseKmPrice other)
        {
            return _price == other.GetPrice();
        }
    }
}
