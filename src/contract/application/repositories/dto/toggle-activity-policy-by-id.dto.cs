

using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace ProvidersMicroservice.src.contract.application.repositories.dto
{
    public record ToggleActivityPolicyByIdDto
    (
        ContractId contractId,
        PolicyId policyId
    );
}
