

using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace ProvidersMicroservice.src.contract.application.repositories.dto
{
    public record SavePolicyDto
    (
        ContractId contractId,
        Policy policy
    );
}
