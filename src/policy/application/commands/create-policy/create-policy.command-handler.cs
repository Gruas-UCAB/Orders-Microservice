using MongoDB.Driver;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.policy.application.commands.create_policy.types;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.domain;
using OrdersMicroservice.src.policy.domain.value_objects;


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

                var id = _idGenerator.GenerateId();
                var name = data.Name;
                var monetaryCoverage = data.MonetaryCoverage;
                var kmCoverage = data.KmCoverage;
                

                var policy = Policy.Create(
                    new PolicyId(id),
                    new PolicyName(name),
                    new PolicyMonetaryCoverage(monetaryCoverage),
                    new PolicyKmCoverage(kmCoverage)
                    
                    );
                var events = policy.PullEvents();
                await _policyRepository.SavePolicy(policy);
                return Result<CreatePolicyResponse>.Success(new CreatePolicyResponse(id));
            } catch (Exception e)
            {
                return Result<CreatePolicyResponse>.Failure(e);
            }
        }
    }
}
