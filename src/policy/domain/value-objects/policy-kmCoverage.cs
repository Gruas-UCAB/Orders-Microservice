


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyKmCoverage : IValueObject<PolicyKmCoverage>
{
 private readonly string _kmCoverage; // kmCoverage string or int

        public PolicyKmCoverage(string kmCoverage)
        {
            if (kmCoverage.Length < 2 || kmCoverage.Length > 50)
            {
                throw new InvalidPolicyKmCoverageException();
            }
            this._kmCoverage = kmCoverage;
        }
        public string GetKmCoverage()
        {
            return this._kmCoverage;
        }
        public bool Equals(PolicyKmCoverage other)
        {
            return _kmCoverage == other.GetKmCoverage();
        }
}
