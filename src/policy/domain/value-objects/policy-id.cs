


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicytId : IValueObject<PolicytId>
{
    private readonly string _id;
    public PolicytId(string id)
    {
        if (UUIDValidator.IsValid(id))
        {
            _id = id;
        }
        else
        {
            throw new InvalidPolicyIdException();
        }
    }

    public string GetId()
    {
        return _id;
    }

    public bool Equals(PolicytId other)
    {
        return _id == other.GetId();
    }
}
