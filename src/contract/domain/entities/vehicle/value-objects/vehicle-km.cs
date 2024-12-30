using OrdersMicroservice.Core.Domain;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleKm : IValueObject<VehicleKm>
    {
        public double Km { get; }

        public VehicleKm(double km)
        {
            if (km < 0)
            {
                throw new ArgumentException("Kilometers cannot be negative", nameof(km));
            }

            Km = km;
        }

    public double GetKm() {
        return this.Km;
    }

        public bool Equals(VehicleKm other)
        {
            if (other == null) return false;
            return Km == other.Km;
        }

        public override bool Equals(object obj)
        {
            if (obj is VehicleKm other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() => Km.GetHashCode();
    }

