namespace OrdersMicroservice.src.policy.application.repositories.dto
{
    public record GetAllPoliciesDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
