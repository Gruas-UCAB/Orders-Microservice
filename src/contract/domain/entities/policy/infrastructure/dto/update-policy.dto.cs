namespace OrdersMicroservice.src.contract.domain.entities.policy.infrastructure.dto
{
    public record UpdatePolicyDto
    (
        string? Name,
        decimal MonetaryCoverage,
        decimal KmCoverage
    );
}
