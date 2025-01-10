using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.user.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.application.commands.update_contract.exceptions;
using MongoDB.Driver;

namespace OrdersMicroservice.src.contract.application.commands.update_contract
{
    public class UpdatePolicyByIdCommandHandler(IPolicyRepository policyRepository) : IApplicationService<UpdatePolicyByIdCommand, UpdatePolicyByIdResponse>
    {
        private readonly IPolicyRepository _policyRepository = policyRepository;
        public async Task<Result<UpdatePolicyByIdResponse>> Execute(UpdatePolicyByIdCommand data)
        {
            try
            {
                if (data.Name == null && data.BaseKmPrice == null && data.MonetaryCoverage == null && data.KmCoverage == null)
                {
                    return Result<UpdatePolicyByIdResponse>.Failure(new InvalidPolicyUpdateDataException());
                }
                
                var policyFound = await _policyRepository.GetPolicyById(new PolicyId(data.Id));
                if (!policyFound.HasValue())
                {
                    return Result<UpdatePolicyByIdResponse>.Failure(new PolicyNotFoundException());
                }
                var policy = policyFound.Unwrap();
                if (data.Name != null)
                {
                    policy.UpdateName(data.Name);
                }
                if (data.MonetaryCoverage != null)
                {
                    policy.UpdateMonetaryCoverage((decimal)data.MonetaryCoverage);
                }
                if (data.KmCoverage != null)
                {
                    policy.UpdateKmCoverage((decimal)data.KmCoverage);
                }
                if (data.BaseKmPrice != null)
                {
                    policy.UpdateBaseKmPrice((decimal)data.BaseKmPrice);
                }
                await _policyRepository.UpdatePolicy(policy);

                return Result<UpdatePolicyByIdResponse>.Success(new UpdatePolicyByIdResponse(policy.GetId()));
            }
            catch (Exception e) 
            {
                return Result<UpdatePolicyByIdResponse>.Failure(e);
            }
        }
    }
}
