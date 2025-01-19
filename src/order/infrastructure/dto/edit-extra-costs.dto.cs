using OrdersMicroservice.src.order.application.commands.edit_order_extra_costs.types;

namespace OrdersMicroservice.src.order.infrastructure.dto
{
    public record EditExtraCostsDto
    (
        List<ExtraCostEditedDto> ExtraCosts
    );
}
