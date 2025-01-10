using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.exceptions
{
    public class InvalidContractException : DomainException
    {
        public InvalidContractException() : base("Invalid contract")
        {
        }
    }
}
