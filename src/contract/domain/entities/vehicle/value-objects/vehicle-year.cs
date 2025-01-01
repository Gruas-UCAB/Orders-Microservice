using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

public class VehicleYear : IValueObject<VehicleYear>
{
    private readonly int _year;

    public VehicleYear(int year)
    {
        if (year < 1886 || year > DateTime.Now.Year + 1)
        {
        throw new InvalidVehicleYearException();
        }
        _year = year;
    }

    public int GetYear() => _year;

    public bool Equals(VehicleYear other)
    {
        return _year == other.GetYear();
    }
}



