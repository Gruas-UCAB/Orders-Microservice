using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;

namespace OrdersMicroservice.src.contract.domain.entities.policy
{
    public class Policy : Entity<PolicyId> 
    {
        private PolicyName _name;
        private PolicyMonetaryCoverage _monetaryCoverage;
        private PolicyKmCoverage _kmCoverage;
        private PolicyBaseKmPrice _baseKmPrice;

        public Policy(PolicyId id, PolicyName name, PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage  kmCoverage, PolicyBaseKmPrice baseKmPrice) : base(id)
        {
            _name = name;
            _monetaryCoverage = monetaryCoverage;
            _kmCoverage = kmCoverage;
            _baseKmPrice = baseKmPrice;
        }

        public string GetId()
        {
            return _id.GetId();
        }


        public string GetName()
        {
            return _name.GetName();
        }

        public decimal GetMonetaryCoverage()
        {
            return _monetaryCoverage.GetMonetaryCoverage();
        }

        public decimal GetkmCoverage()
        {
            return _kmCoverage.GetKmCoverage();
        }

        public decimal GetBaseKmPrice()
        {
            return _baseKmPrice.GetPrice();
        }
        public void UpdateName(string name)
        {
            _name = new PolicyName(name);
        }
        public void UpdateMonetaryCoverage(decimal monetaryCoverage)
        {
            _monetaryCoverage = new PolicyMonetaryCoverage(monetaryCoverage);
        }

        public void UpdateKmCoverage(decimal kmCoverage)
        {
            _kmCoverage = new PolicyKmCoverage(kmCoverage);
        }

        public void UpdateBaseKmPrice(decimal baseKmPrice)
        {
            _baseKmPrice = new PolicyBaseKmPrice(baseKmPrice);
        }
    }
}
