namespace OrdersMicroservice.src.policy.infrastructure.dto
{
    public record UpdatePolicyDto
    (
        string? Name,
        string? MonetaryCoverage,
        string? KmCoverage
    );
}
