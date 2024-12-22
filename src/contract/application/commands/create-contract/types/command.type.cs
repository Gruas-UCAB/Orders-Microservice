namespace OrdersMicroservice.src.contract.application.commands.create_contract.types
{
    public record CreateContractCommand(
        decimal ContractNumber,
        DateTime ContractExpirationDate,
        string PolicyId,
        string VehicleId
        );
}
