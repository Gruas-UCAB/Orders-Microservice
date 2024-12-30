using OrdersMicroservice.Core.Domain;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleColor : IValueObject<VehicleColor>
    {
        public string Color { get; }

        public VehicleColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color cannot be null or empty", nameof(color));
            }

            Color = color;
        }

        public string GetColor() => Color;

        public bool Equals(VehicleColor other)
        {
            if (other == null) return false;
            return Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (obj is VehicleColor other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() => Color.GetHashCode();
    }



