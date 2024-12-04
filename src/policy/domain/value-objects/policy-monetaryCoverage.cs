


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyMonetaryCoverage : IValueObject<PolicyMonetaryCoverage>
{
 private readonly string _monetaryCoverage;// string o int ?

        public PolicyMonetaryCoverage(string monetaryCoverage)
        {
            if (monetaryCoverage.Length < 2 || monetaryCoverage.Length > 50)
            {
                throw new InvalidPolicyMonetaryCoverageException();
            }
            this._monetaryCoverage = monetaryCoverage;
        }
        public string GetMonetaryCoverage()
        {
            return this._monetaryCoverage;
        }
        public bool Equals(PolicyMonetaryCoverage other)
        {
            return _monetaryCoverage == other.GetMonetaryCoverage();
        }
}
