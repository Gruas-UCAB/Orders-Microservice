using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.user.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.application.commands.update_contract.exceptions;

namespace OrdersMicroservice.src.contract.application.commands.update_contract
{
    public class UpdateContractByIdCommandHandler(IContractRepository contractRepository, IPolicyRepository policyRepository) : IApplicationService<UpdateContractCommand, UpdateContractByIdResponse>
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPolicyRepository _policyRepository = policyRepository;
        public async Task<Result<UpdateContractByIdResponse>> Execute(UpdateContractCommand data)
        {
            try
            {
                if (data.PolicyId == null && data.ExpirationDate == null)
                {
                    return Result<UpdateContractByIdResponse>.Failure(new InvalidContractUpdateDataException());
                }
                var contractFound = await _contractRepository.GetContractById(new ContractId(data.ContractId));
                if ( !contractFound.HasValue())
                {
                    return Result<UpdateContractByIdResponse>.Failure(new ContractNotFoundException());
                }
                var contract = contractFound.Unwrap();
                if (data.PolicyId != null)
                {
                    var policyFound = await _policyRepository.GetPolicyById(new PolicyId(data.PolicyId));
                    var policy = policyFound.Unwrap();
                    contract.UpdateContractPolicy(policy);
                }
                if (data.ExpirationDate.HasValue)
                {
                    contract.UpdateExpirationDate(new ContractExpitionDate(data.ExpirationDate.Value));
                }

                await _contractRepository.UpdateContract(contract);

                return Result<UpdateContractByIdResponse>.Success(new UpdateContractByIdResponse(contract.GetId()));
            }
            catch (Exception e) 
            {
                return Result<UpdateContractByIdResponse>.Failure(e);
            }
        }
    }
}
