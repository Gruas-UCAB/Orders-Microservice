using OrdersMicroservice.src.vehicle.application.commands.create_vehicle.types;
using OrdersMicroservice.src.vehicle.application.repositories;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.vehicle.domain;
using OrdersMicroservice.src.vehicle.domain.value_objects;

namespace OrdersMicroservice.src.vehicle.application.commands.create_vehicle
{
    public class CreateVehicleCommandHandler(IIdGenerator<string> idGenerator, IVehicleRepository vehicleRepository) : IApplicationService<CreateVehicleCommand, CreateVehicleResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        public async Task<Result<CreateVehicleResponse>> Execute(CreateVehicleCommand data)
        {
            try
            {
                var id = _idGenerator.GenerateId();
                var licensePlate = data.licensePlate;
                var brand = data.brand;
                var model = data.model;
                var year = data.year;
                var color = data.color;
                var km = data.km;

                var vehicle = Vehicle.Create(
                    new VehicleId(id),
                    new VehicleLicensePlate(licensePlate),
                    new VehicleBrand(brand),
                    new VehicleModel(model),
                    new VehicleYear(year),
                    new VehicleColor(color),
                    new VehicleKm(km)
                );

                var events = vehicle.PullEvents();
                await _vehicleRepository.SaveVehicle(vehicle);
                return Result<CreateVehicleResponse>.Success(new CreateVehicleResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateVehicleResponse>.Failure(e);
            }
        }
    }
}
