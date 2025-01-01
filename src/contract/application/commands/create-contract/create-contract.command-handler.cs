using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;
using System.Runtime.InteropServices;



namespace OrdersMicroservice.src.contract.application.commands.create_contract
{
    public class CreateContractCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository, IPolicyRepository policyRepository) : IApplicationService<CreateContractCommand, CreateContractResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPolicyRepository _policyRepository = policyRepository;

        public async Task<Result<CreateContractResponse>> Execute(CreateContractCommand data)
        {
            try
            {   var policyFound = await _policyRepository.GetPolicyById(new PolicyId(data.PolicyId));
                if (!policyFound.HasValue()){
                    throw new PolicyNotFoundException();
                }
                var policy = policyFound.Unwrap();
                
                var vehicle = new Vehicle(
                    new VehicleId(_idGenerator.GenerateId()),
                    new VehicleLicensePlate(data.LicensePlate),
                    new VehicleBrand(data.Brand),
                    new VehicleModel(data.Model),
                    new VehicleYear(data.Year),
                    new VehicleColor(data.Color),
                    new VehicleKm(data.Km),
                    new VehicleOwnerDni(data.OwnerDni),
                    new VehicleOwnerName(data.OwnerName)
                );
                var contract = Contract.Create(
                        new ContractId(_idGenerator.GenerateId()),
                        new NumberContract(data.ContractNumber),
                        new ContractExpitionDate(data.ContractExpirationDate),
                        vehicle,
                        policy
                    );
                await _contractRepository.SaveContract(contract);
                return Result<CreateContractResponse>.Success(new CreateContractResponse(contract.GetId()));
            }
            catch (Exception e)
            {
                return Result<CreateContractResponse>.Failure(e);
            }
        }
    }
}
