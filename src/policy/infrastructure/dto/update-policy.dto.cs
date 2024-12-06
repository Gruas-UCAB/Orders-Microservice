namespace OrdersMicroservice.src.policy.infrastructure.dto
{
    public record UpdatePolicyDto
    (
        string? Name,
        decimal MonetaryCoverage,
        decimal KmCoverage
    );
}
