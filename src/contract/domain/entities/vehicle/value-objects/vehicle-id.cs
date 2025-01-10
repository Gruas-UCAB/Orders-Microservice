using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

public class VehicleId :IValueObject<VehicleId>
{
    private readonly string _id;

    public VehicleId(string id)
    {
        if (UUIDValidator.IsValid(id))
        {
            _id = id;
        }
        else
        {
            throw new InvalidVehicleIdException();
        }
    }

    public string GetId()
    {
        return _id;
    }

    public bool Equals(VehicleId other)
    { 
        return _id == other.GetId();
    }
}
