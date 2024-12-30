using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;

using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;



namespace OrdersMicroservice.src.contract.application.commands.create_contract
{
    public class CreateContractCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository) : IApplicationService<CreateContractCommand, CreateContractResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;

        private readonly IContractRepository _contractRepository = contractRepository;



        public async Task<Result<CreateContractResponse>> Execute(CreateContractCommand data)
        {
            try
            {
     
                var id = _idGenerator.GenerateId();
                var numberContract = data.ContractNumber;
                var expirationDate = data.ContractExpirationDate;
                var licensePlate = data.licensePlate;
                var brand = data.brand;
                var model = data.model;
                var year = data.year;
                var color = data.color;
                var km = data.km;


                var contract = Contract.Create(
                    new ContractId(id),
                    new NumberContract(numberContract),
                    new ContractExpitionDate(expirationDate),
                    new Vehicle(
                        new VehicleId(id),
                        new VehicleLicensePlate(licensePlate),
                        new VehicleBrand(brand),
                        new VehicleModel(model),
                        new VehicleYear(year),
                        new VehicleColor(color),
                        new VehicleKm(km)
                    )
                    /*new Policy(
                        new PolicyId(id)
                        )*/

                    );
                /*var events = contract.PullEvents();*/
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
