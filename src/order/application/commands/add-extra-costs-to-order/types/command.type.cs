using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order.types
{
    public record ExtraCostDto
    (
        string Id,
        string Description,
        decimal Price
    );
    public record AddExtraCostsToOrderCommand
    (
        string OrderId,
        List<ExtraCostDto> ExtraCosts
    );
}
