namespace ProvidersMicroservice.src.contract.application.repositories.dto
{
    public record GetPolicyByIdDto
    (
        string contractId,
        string policyId
    );
}
