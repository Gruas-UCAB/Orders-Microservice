using OrdersMicroservice.Core.Domain;
namespace OrdersMicroservice.src.contract.domain.exceptions
{
    public class InvalidNumberContractException : DomainException
{
    public InvalidNumberContractException() : base("Invalid contract Number")
    {
    }
}
}
