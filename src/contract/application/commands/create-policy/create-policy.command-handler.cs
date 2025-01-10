using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_policy.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;


namespace OrdersMicroservice.src.policy.application.commands.create_policy
{
    public class CreatePolicyCommandHandler(IIdGenerator<string> idGenerator, IPolicyRepository policyRepository) : IApplicationService<CreatePolicyCommand, CreatePolicyResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IPolicyRepository _policyRepository = policyRepository;
    

        public async Task<Result<CreatePolicyResponse>> Execute(CreatePolicyCommand data)
        {
            try
            {
                var policy = new Policy(
                        new PolicyId(_idGenerator.GenerateId()),
                        new PolicyName(data.Name),
                        new PolicyMonetaryCoverage(data.MonetaryCoverage),
                        new PolicyKmCoverage(data.KmCoverage),
                        new PolicyBaseKmPrice(data.BaseKmPrice)
                    );
                await _policyRepository.SavePolicy(policy);
                return Result<CreatePolicyResponse>.Success(new CreatePolicyResponse(policy.GetId()));
            } catch (Exception e)
            {
                return Result<CreatePolicyResponse>.Failure(e);
            }
        }
    }
}
