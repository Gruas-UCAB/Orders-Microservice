


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.exceptions;

namespace OrdersMicroservice.src.policy.domain.value_objects;
public class PolicyId : IValueObject<PolicyId>
{
    private readonly string _id;
    public PolicyId(string id)
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

    public bool Equals(PolicyId other)
    {
        return _id == other.GetId();
    }
}
