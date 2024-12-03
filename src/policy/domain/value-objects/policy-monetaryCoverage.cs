


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyMonetaryCoverage : IValueObject<PolicyMonetaryCoverage>
{
 private readonly string _name;

        public PolicyMonetaryCoverage(string name)
        {
            if (name.Length < 2 || name.Length > 50)
            {
                throw new InvalidPolicyMonetaryCoverageException();
            }
            this._name = name;
        }
        public string GetName()
        {
            return this._name;
        }
        public bool Equals(PolicyMonetaryCoverage other)
        {
            return _name == other.GetName();
        }
}
