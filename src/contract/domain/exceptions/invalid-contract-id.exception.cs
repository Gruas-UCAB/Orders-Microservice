using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.exceptions
{
    public class InvalidContractIdException : DomainException
{
    public InvalidContractIdException() : base("Invalid contract ID")
    {
    }
}
}
