using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;

using OrdersMicroservice.src.policy.application.commands.update_policy.types;

namespace OrdersMicroservice.src.contract.application.repositories
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
