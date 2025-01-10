using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class IncidentType : IValueObject<IncidentType>
    {
        private readonly List<string> _validTypes = ["accidente automovilistico", "incendio", "volcamiento", "vehiculo accidentado"];
        private readonly string _type;

        public IncidentType(string type)
        {
            if (!_validTypes.Contains(type))
            {
                throw new InvalidIncidentTypeException();
            }
            _type = type;
        }

        public string GetIncidentType()
        {
            return _type;
        }
        public bool Equals(IncidentType other)
        {
            return _type == other.GetIncidentType();
        }
    }
}
