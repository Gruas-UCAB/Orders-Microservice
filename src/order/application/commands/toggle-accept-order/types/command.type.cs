namespace OrdersMicroservice.src.order.application.commands.accept_order.types
{
    public record ToggleAcceptOrderCommand
    (
        string OrderId,
        bool Accepted
    );
}
