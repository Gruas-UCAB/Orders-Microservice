


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
public class PolicyMonetaryCoverage : IValueObject<PolicyMonetaryCoverage>
{
 private readonly decimal _monetaryCoverage;

        public PolicyMonetaryCoverage(decimal monetaryCoverage)
        {
            if (monetaryCoverage < 0 )
            {
                throw new InvalidPolicyMonetaryCoverageException();
            }
            this._monetaryCoverage = monetaryCoverage;
        }
        public decimal GetMonetaryCoverage()
        {
            return this._monetaryCoverage;
        }
        public bool Equals(PolicyMonetaryCoverage other)
        {
            return _monetaryCoverage == other.GetMonetaryCoverage();
        }
}
