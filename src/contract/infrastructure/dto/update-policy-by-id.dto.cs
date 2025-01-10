namespace OrdersMicroservice.src.contract.infrastructure.dto
{
    public record UpdatePolicyByIdDto
    (
        string ?Name,
        decimal ?MonetaryCoverage,
        decimal ?KmCoverage,
        decimal ?BaseKmPrice
    );
}
