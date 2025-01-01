namespace OrdersMicroservice.src.contract.infrastructure.dto
{
    public record UpdateContractDto
    (
        string? PolicyId,
        DateTime? ExpirationDate
    );
}
