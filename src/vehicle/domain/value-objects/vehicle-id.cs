using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.vehicle.domain.exceptions;
      

namespace OrdersMicroservice.src.vehicle.domain.value_objects;

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
