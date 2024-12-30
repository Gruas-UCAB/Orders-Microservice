namespace OrdersMicroservice.src.contract.application.commands.create_vehicle.types
{
    public record CreateVehicleCommand
        (
        string ContractId,
        string licensePlate,
        string brand,
        string model,
        int year,
        string color,
        double km
        );
}
