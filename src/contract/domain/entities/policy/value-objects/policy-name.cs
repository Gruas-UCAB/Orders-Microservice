


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
public class PolicyName : IValueObject<PolicyName>
{
 private readonly string _name;

        public PolicyName(string name)
        {
            if (name.Length < 2 || name.Length > 50)
            {
                throw new InvalidPolicyNameException();
            }
            this._name = name;
        }
        public string GetName()
        {
            return this._name;
        }
        public bool Equals(PolicyName other)
        {
            return _name == other.GetName();
        }
}
