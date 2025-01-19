using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.application.commands.edit_order_extra_costs.types
{
    public record ExtraCostEditedDto
    (   
        string Id,
        decimal Price
    );
    public record EditOrderExtraCostsCommand
    (
        string OrderId,
        List<ExtraCostEditedDto> ExtraCosts
    );
}
