


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
public class PolicyKmCoverage : IValueObject<PolicyKmCoverage>
{
 private readonly decimal _kmCoverage;

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
