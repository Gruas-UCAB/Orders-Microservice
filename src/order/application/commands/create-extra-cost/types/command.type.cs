namespace OrdersMicroservice.src.order.application.commands.create_extra_cost.types
{
    public record CreateExtraCostCommand
    (
        decimal DefaultPrice,
        string Description
    );
}
