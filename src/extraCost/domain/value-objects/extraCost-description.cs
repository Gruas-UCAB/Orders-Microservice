


using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.extracost.domain.exceptions;

namespace OrdersMicroservice.src.extracost.domain.value_objects;
public class ExtraCostDescription : IValueObject<ExtraCostDescription>
{
 private readonly string _description;

        public ExtraCostDescription(string description)
        {
            if (description.Length < 2 || description.Length > 50)
            {
                throw new InvalidExtraCostDescriptionException();
            }
            this._description = description;
        }
        public string GetExtraCostDescription()
        {
            return this._description;
        }
        public bool Equals(ExtraCostDescription other)
        {
            return _description == other.GetExtraCostDescription();
        }
}
