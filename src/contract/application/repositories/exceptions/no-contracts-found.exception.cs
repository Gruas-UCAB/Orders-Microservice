namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class NoContractsFoundException : Exception
    {
        public NoContractsFoundException() : base("No contracts found")
        {
        }
    }
}
