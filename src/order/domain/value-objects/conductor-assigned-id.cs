using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class ConductorAssignedId : IValueObject<ConductorAssignedId>
    {
        private readonly string _id;
        public ConductorAssignedId(string id)
        {
            if (!UUIDValidator.IsValid(id))
            {
                throw new InvalidConductorAssignedIdException();
            }
            _id = id;
        }
        public string GetId()
        {
            return _id;
        }
        public bool Equals(ConductorAssignedId other)
        {
            return _id == other.GetId();
        }
    }
}
