using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.policy.application.commands.update_policy.types;
using OrdersMicroservice.src.policy.application.repositories.dto;
using OrdersMicroservice.src.policy.domain;
using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.policy.infrastructure.dto;
namespace OrdersMicroservice.src.policy.application.repositories
{
    public interface IPolicyRepository
    {
        public Task<Policy> SavePolicy(Policy policy);
        public Task<_Optional<List<Policy>>> GetAllPolicies();
        public Task<_Optional<Policy>> GetPolicyById(PolicyId id);
        public Task<PolicyId> UpdatePolicyById(UpdatePolicyByIdCommand command);
        public Task<PolicyId> ToggleActivityPolicyById(PolicyId id);

    }
}
