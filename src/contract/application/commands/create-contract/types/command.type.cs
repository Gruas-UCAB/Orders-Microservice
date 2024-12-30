namespace OrdersMicroservice.src.contract.application.commands.create_contract.types
{
    public record CreateContractCommand(

        decimal ContractNumber,
        DateTime ContractExpirationDate,

        string ContractId,
        string licensePlate,
        string brand,
        string model,
        int year,
        string color,
        double km,

        string PolicyId

        );
}
