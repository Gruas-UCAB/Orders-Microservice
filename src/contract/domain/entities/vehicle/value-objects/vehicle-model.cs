using OrdersMicroservice.Core.Domain;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

    public class VehicleModel : IValueObject<VehicleModel>
    {
        public string Model { get; }

        public VehicleModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Model cannot be null or empty", nameof(model));
            }

            Model = model;
        }

        public string GetModel() => Model;

        public bool Equals(VehicleModel other)
        {
            if (other == null) return false;
            return Model == other.Model;
        }

        public override bool Equals(object obj)
        {
            if (obj is VehicleModel other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() => Model.GetHashCode();
    }


