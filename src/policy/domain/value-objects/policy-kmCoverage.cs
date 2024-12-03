


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyKmCoverage : IValueObject<PolicyKmCoverage>
{
 private readonly string _name;

        public PolicyKmCoverage(string name)
        {
            if (name.Length < 2 || name.Length > 50)
            {
                throw new InvalidPolicyKmCoverageException();
            }
            this._name = name;
        }
        public string GetName()
        {
            return this._name;
        }
        public bool Equals(PolicyKmCoverage other)
        {
            return _name == other.GetName();
        }
}
