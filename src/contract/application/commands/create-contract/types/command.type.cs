namespace OrdersMicroservice.src.contract.application.commands.create_contract.types
{
    public record CreateContractCommand(

        int ContractNumber,
        DateTime ContractExpirationDate,
        string LicensePlate,
        string Brand,
        string Model,
        int Year,
        string Color,
        int Km,
        int OwnerDni,
        string OwnerName,
        string PolicyId
        );
}
