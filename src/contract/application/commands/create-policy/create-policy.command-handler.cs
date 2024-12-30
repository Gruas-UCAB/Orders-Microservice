using MongoDB.Driver;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_policy.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;
using ProvidersMicroservice.src.contract.application.repositories.dto;



namespace OrdersMicroservice.src.policy.application.commands.create_policy
{
    public class CreatePolicyCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository /*,IPolicyRepository policyRepository*/) : IApplicationService<CreatePolicyCommand, CreatePolicyResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
       /* private readonly IPolicyRepository _policyRepository = policyRepository;*/
        private readonly IContractRepository _contractRepository = contractRepository;
    

        public async Task<Result<CreatePolicyResponse>> Execute(CreatePolicyCommand data)
        {
            try
            {
                var contractId = new ContractId(data.ContractId);
                var contract = await _contractRepository.GetContractById(contractId);

                if (!contract.HasValue())
                {
                    return Result<CreatePolicyResponse>.Failure(new PolicyNotFoundException());
                }

                var id = _idGenerator.GenerateId();
                var name = data.Name;
                var monetaryCoverage = data.MonetaryCoverage;
                var kmCoverage = data.KmCoverage;

                var policyWithContract = contract.Unwrap();
                var policy = policyWithContract.AddPolicy(
                    new PolicyId(id),
                    new PolicyName(name),
                    new PolicyMonetaryCoverage(monetaryCoverage),
                    new PolicyKmCoverage(kmCoverage)
                    
                    );
                
                await _contractRepository.SavePolicy(new SavePolicyDto(contractId, policy));
                return Result<CreatePolicyResponse>.Success(new CreatePolicyResponse(id));
            } catch (Exception e)
            {
                return Result<CreatePolicyResponse>.Failure(e);
            }
        }
    }
}
