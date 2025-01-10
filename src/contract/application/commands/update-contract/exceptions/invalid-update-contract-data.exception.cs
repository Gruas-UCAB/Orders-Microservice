namespace OrdersMicroservice.src.contract.application.commands.update_contract.exceptions
{
    public class InvalidContractUpdateDataException : ApplicationException
    {
        public InvalidContractUpdateDataException() : base("The expiration date and the policy can't be null at the same time.")
        {
        }
    }
}
