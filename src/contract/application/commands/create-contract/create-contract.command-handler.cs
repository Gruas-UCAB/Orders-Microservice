using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;

using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.application.repositories.exceptions;

using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.vehicle.application.repositories;
using OrdersMicroservice.src.vehicle.application.repositories.exceptions;
using OrdersMicroservice.src.vehicle.domain.value_objects;

namespace OrdersMicroservice.src.contract.application.commands.create_contract
{
    public class CreateContractCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository,IVehicleRepository vehicleRepository, IPolicyRepository policyRepository) : IApplicationService<CreateContractCommand, CreateContractResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;

        private readonly IContractRepository _contractRepository = contractRepository;

        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        private readonly IPolicyRepository _IPolicyRepository = policyRepository;

        public async Task<Result<CreateContractResponse>> Execute(CreateContractCommand data)
        {
            try
            {
             var vehicle = await _vehicleRepository.GetVehicleById(new VehicleId(data.VehicleId));
                if (!vehicle.HasValue())
                {
                    return Result<CreateContractResponse>.Failure(new VehicleNotFoundException());
                }
            var policy = await _IPolicyRepository.GetPolicyById(new PolicyId(data.PolicyId));
                if (!policy.HasValue())
                {
                    return Result<CreateContractResponse>.Failure(new PolicyNotFoundException());
                }
                var id = _idGenerator.GenerateId();
                var numberContract = data.ContractNumber;
                var expirationDate = data.ContractExpirationDate;
                var vehicleId = data.VehicleId;
                var policyId = data.PolicyId;

                var contract = Contract.Create(
                    new ContractId(id),
                    new NumberContract(numberContract),
                    new ContractExpitionDate(expirationDate),
                    new VehicleId(vehicleId),
                    new PolicyId(policyId)

                    
                    );
                var events = contract.PullEvents();
                await _contractRepository.SaveContract(contract);
                return Result<CreateContractResponse>.Success(new CreateContractResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateContractResponse>.Failure(e);
            }
        }
    }
}
