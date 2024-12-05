


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyMonetaryCoverage : IValueObject<PolicyMonetaryCoverage>
{
 private readonly decimal _monetaryCoverage;// string o int ?

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
