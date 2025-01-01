using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;

namespace OrdersMicroservice.src.contract.application.repositories
{
    public interface IPolicyRepository
    {
        public Task<Policy> SavePolicy(Policy policy);
        public Task<_Optional<List<Policy>>> GetAllPolicies(GetAllPolicesDto data);
        public Task<_Optional<Policy>> GetPolicyById(PolicyId id);
        public Task<PolicyId> UpdatePolicy(Policy policy);
    }
}
