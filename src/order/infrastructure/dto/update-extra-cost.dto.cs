namespace OrdersMicroservice.src.order.infrastructure.dto
{
    public record UpdateExtraCostDto
    (
        string? Description,
        decimal? DefaultPrice
    );
}
