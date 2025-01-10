namespace OrdersMicroservice.src.order.application.commands.create_order.types
{
    public record CreateOrderCommand
    (
        DateTime Date,
        string IncidentType,
        string Destination,
        string Location,
        string OrderDispatcherId,
        string ContractId
    );
}
