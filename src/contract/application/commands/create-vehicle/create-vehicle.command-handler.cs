
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_vehicle.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;
using ProvidersMicroservice.src.contract.application.repositories.dto;


namespace OrdersMicroservice.src.vehicle.application.commands.create_vehicle
{
    public class CreateVehicleCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository/*, IVehicleRepository vehicleRepository*/) : IApplicationService<CreateVehicleCommand, CreateVehicleResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
       /* private readonly IVehicleRepository _vehicleRepository = vehicleRepository;*/
        private readonly IContractRepository _contractRepository = contractRepository;
        public async Task<Result<CreateVehicleResponse>> Execute(CreateVehicleCommand data)
        {
            try
            {
                var contractId = new ContractId(data.ContractId);
                var contract = await _contractRepository.GetContractById(contractId);


                if (!contract.HasValue())
                {
                    return Result<CreateVehicleResponse>.Failure(new VehicleNotFoundException());
                }
                var id = _idGenerator.GenerateId();
                var licensePlate = data.licensePlate;
                var brand = data.brand;
                var model = data.model;
                var year = data.year;
                var color = data.color;
                var km = data.km;

                var contractWithContract = contract.Unwrap();
                var vehicle = contractWithContract.AddVehicle(
                    new VehicleId(id),
                    new VehicleLicensePlate(licensePlate),
                    new VehicleBrand(brand),
                    new VehicleModel(model),
                    new VehicleYear(year),
                    new VehicleColor(color),
                    new VehicleKm(km)
                );

               
                await _contractRepository.SaveVehicle(new SaveVehicleDto(contractId, vehicle));
                return Result<CreateVehicleResponse>.Success(new CreateVehicleResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateVehicleResponse>.Failure(e);
            }
        }
    }
}
