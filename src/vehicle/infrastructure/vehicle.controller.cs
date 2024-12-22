using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.src.vehicle.application.commands.create_vehicle;
using OrdersMicroservice.src.vehicle.application.commands.create_vehicle.types;
using OrdersMicroservice.src.vehicle.application.repositories;
using OrdersMicroservice.src.vehicle.application.repositories.exceptions;
using OrdersMicroservice.src.vehicle.infrastructure.repositories;
using OrdersMicroservice.src.vehicle.infrastructure.validators;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.core.Infrastructure;


namespace OrdersMicroservice.src.vehicle.infrastructure
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository = new MongoVehicleRepository();
        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
        {
            var validator = new CreateVehicleCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateVehicleCommandHandler(_idGenerator, _vehicleRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();

            if (!vehicles.HasValue())
            {
                return NotFound(
                    new { errorMessage = new VehicleNotFoundException().Message }
                    );
            }

            var vehicleList = vehicles.Unwrap().Select(d => new
            {
                Id = d.GetId(),
                licensePlate = d.GetLicensePlate(),
                brand = d.GetBrand(),
                model = d.GetModel(),
                year = d.GetYear(),
                color = d.GetColor(),
                km = d.GetKm()
            }).ToList();

            return Ok(vehicleList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(string id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(new domain.value_objects.VehicleId(id));
            if (!vehicle.HasValue())
            {
                return NotFound(
                    new { errorMessage = new VehicleNotFoundException().Message }
                );
            }

            var vehicleData = new
            {
                Id = vehicle.Unwrap().GetId(),
                licensePlate = vehicle.Unwrap().GetLicensePlate(),
                brand = vehicle.Unwrap().GetBrand(),
                model = vehicle.Unwrap().GetModel(),
                year = vehicle.Unwrap().GetYear(),
                color = vehicle.Unwrap().GetColor(),
                km = vehicle.Unwrap().GetKm()
            };

            return Ok(vehicleData);
        }
    }
}
