using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleColor : IValueObject<VehicleColor>
    {
    private readonly string _color;

        public VehicleColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
            throw new InvalidVehicleColorException();
            }

            _color = color;
        }

        public string GetColor() => _color;

        public bool Equals(VehicleColor other)
        {
            return _color == other.GetColor();
        }
    }



