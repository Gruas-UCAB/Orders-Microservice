using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.extracost.domain.exceptions
{
    public class InvalidExtraCostException : DomainException
    {
        public InvalidExtraCostException() : base("Invalid cost")
        {
        }
    }
}
