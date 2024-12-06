namespace OrdersMicroservice.src.extracost.infrastructure.dto
{
    public record UpdateExtraCostDto
    (
        string? Description,
        decimal Price
        
    );
}
