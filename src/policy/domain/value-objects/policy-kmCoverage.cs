


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyKmCoverage : IValueObject<PolicyKmCoverage>
{
 private readonly decimal _kmCoverage; // kmCoverage string or numero

        public PolicyKmCoverage(decimal kmCoverage)
        {
            if (kmCoverage < 0 )
            {
                throw new InvalidPolicyKmCoverageException();
            }
            this._kmCoverage = kmCoverage;
        }
        public decimal GetKmCoverage()
        {
            return this._kmCoverage ;
        }
        public bool Equals(PolicyKmCoverage other)
        {
            return _kmCoverage == other.GetKmCoverage();
        }
}
