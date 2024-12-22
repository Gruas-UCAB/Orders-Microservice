using OrdersMicroservice.src.vehicle.domain.exceptions;
using UsersMicroservice.Core.Domain;

namespace OrdersMicroservice.src.vehicle.domain.value_objects
{
    public class VehicleYear : IValueObject<VehicleYear>
    {
        public int Year { get; }

        public VehicleYear(int year)
        {
            if (year < 1886 || year > DateTime.Now.Year + 1) // The first car was invented in 1886
            {
                throw new ArgumentException("Year is not valid", nameof(year));
            }

            Year = year;
        }

        public int GetYear() => Year;

        public bool Equals(VehicleYear other)
        {
            if (other == null) return false;
            return Year == other.Year;
        }

        public override bool Equals(object obj)
        {
            if (obj is VehicleYear other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode() => Year.GetHashCode();
    }


}
