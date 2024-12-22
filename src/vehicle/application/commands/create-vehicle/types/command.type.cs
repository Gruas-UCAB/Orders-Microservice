namespace OrdersMicroservice.src.vehicle.application.commands.create_vehicle.types
{
    public record CreateVehicleCommand
        (
        string licensePlate,
        string brand,
        string model,
        int year,
        string color,
        double km
        );
}
