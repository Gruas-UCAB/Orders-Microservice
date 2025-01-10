using OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order.types;
using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.infrastructure.dto
{
    public record UpdateOrderDto
    (
        string? ConductorAssignedId,
        decimal? Cost,
        bool? Accepted,
        List<ExtraCostDto>? ExtraCosts,
        decimal? TotalDistance
    );
}
