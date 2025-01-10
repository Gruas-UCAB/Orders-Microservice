namespace OrdersMicroservice.src.order.application.commands.assign_conductor.types
{
    public record AssignConductorCommand
    (
        string OrderId,
        string ConductorId,
        decimal TotalDistance
    );
}
