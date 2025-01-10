using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;

namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
public class VehicleBrand : IValueObject<VehicleBrand>
{
    private readonly string _brand;
    public VehicleBrand(string brand)
    {
        if (string.IsNullOrWhiteSpace(brand) || brand.Length < 3 || brand.Length > 20)
        {
            throw new InvalidVehicleBrandException();
        }
        _brand = brand;
    }
    public string GetBrand() => _brand;
    public bool Equals(VehicleBrand other)
    {
        return _brand == other.GetBrand();
    }
}
