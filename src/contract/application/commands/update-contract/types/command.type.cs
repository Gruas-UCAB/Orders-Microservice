namespace OrdersMicroservice.src.contract.application.commands.update_contract.types
{
    public record UpdateContractCommand
        (
            string ContractId,
            DateTime? ExpirationDate,
            string? PolicyId
        );
}
