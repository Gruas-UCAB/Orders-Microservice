using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

public class VehicleKm : IValueObject<VehicleKm>
{
private readonly int _km;

public VehicleKm(int km)
{
    if (km < 0)
    {
    throw new InvalidVehicleKmException();
    }

    _km = km;
}
public int GetKm() {
    return _km;
}
public bool Equals(VehicleKm other)
{
    return _km == other.GetKm();
}
}

