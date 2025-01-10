namespace OrdersMicroservice.src.contract.application.commands.update_contract.exceptions
{
    public class InvalidPolicyUpdateDataException : ApplicationException
    {
        public InvalidPolicyUpdateDataException() : base("The name, km coverage, monetary coverage and base km price can't be null at the same time.")
        {
        }
    }
}
