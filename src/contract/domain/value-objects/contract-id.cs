


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class ContractId : IValueObject<ContractId>
{
    private readonly string _id;
    public ContractId(string id)
    {
        if (UUIDValidator.IsValid(id))
        {
            _id = id;
        }
        else
        {
            throw new InvalidContractIdException();
        }
    }

    public string GetId()
    {
        return _id;
    }

    public bool Equals(ContractId other)
    {
        return _id == other.GetId();
    }
}
