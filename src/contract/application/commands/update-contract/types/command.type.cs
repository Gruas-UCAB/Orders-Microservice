namespace OrdersMicroservice.src.contract.application.commands.update_contract.types
{
    public class UpdateContractByIdCommand(string Id, decimal NumberContract, DateTime ContractExpirationDate )
    {
        public string Id = Id;
        public decimal NumberContract = NumberContract;
        public DateTime ExpirationDate = ContractExpirationDate;
  
    }
}
