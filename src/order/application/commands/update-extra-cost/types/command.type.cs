namespace OrdersMicroservice.src.order.application.commands.update_extra_cost.types
{
    public record UpdateExtraCostCommand
    (
        string Id,
        string? Description,
        decimal? DefaultPrice
    );
}
