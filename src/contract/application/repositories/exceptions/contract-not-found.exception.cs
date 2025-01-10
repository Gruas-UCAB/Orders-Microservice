namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class ContractNotFoundException : Exception
    {
        public ContractNotFoundException() : base("Contract not found") { }
    }
}
