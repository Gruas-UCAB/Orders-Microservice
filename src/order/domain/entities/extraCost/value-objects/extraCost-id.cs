


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.entities.extraCost.exceptions;

namespace OrdersMicroservice.src.extracost.domain.value_objects;
public class ExtraCostId : IValueObject<ExtraCostId>
{
    private readonly string _id;
    public ExtraCostId(string id)
    {
        if (UUIDValidator.IsValid(id))
        {
            _id = id;
        }
        else
        {
            throw new InvalidExtraCostIdException();
        }
    }

    public string GetExtraCostId()
    {
        return _id;
    }

    public bool Equals(ExtraCostId other)
    {
        return _id == other.GetExtraCostId();
    }
}
