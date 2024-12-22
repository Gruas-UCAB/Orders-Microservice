using OrdersMicroservice.src.vehicle.domain.exceptions;
using OrdersMicroservice.Core.Domain;

public class VehicleBrand : IValueObject<VehicleBrand>
{
    public string Brand { get; }

    public VehicleBrand(string brand)
    {
        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new ArgumentException("Brand cannot be null or empty", nameof(brand));
        }

        Brand = brand;
    }

    public string GetBrand() => Brand;

    public bool Equals(VehicleBrand other)
    {
        if (other == null) return false;
        return Brand == other.Brand;
    }

    public override bool Equals(object obj)
    {
        if (obj is VehicleBrand other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode() => Brand.GetHashCode();
}
