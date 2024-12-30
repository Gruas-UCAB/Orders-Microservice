using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.core.Infrastructure;

using OrdersMicroservice.src.contract.application.commands.update_contract;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;

using OrdersMicroservice.src.contract.application.commands.create_contract;
using OrdersMicroservice.src.contract.application.repositories;

using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.infrastructure.dto;

using OrdersMicroservice.src.contract.infrastructure.validators;


using contractsMicroservice.src.contract.infrastructure.repositories;

using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.infrastructure.repositories;
using OrdersMicroservice.src.contract.application.commands.create_vehicle.types;
using OrdersMicroservice.src.vehicle.application.commands.create_vehicle;
using ProvidersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.policy.application.commands.create_policy;
using MongoDB.Driver;
using OrdersMicroservice.src.contract.application.commands.create_policy.types;

namespace OrdersMicroservice.src.contract.infrastructure
{
    [Route("api/contract")]
    [ApiController]
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository = new MongoContractRepository();
        private readonly IPolicyRepository _policyRepository = new MongoPolicyRepository();

        private readonly IVehicleRepository _vehicleRepository = new MongoVehicleRepository();

        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractCommand command)
        {
            var validator = new CreateContractCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateContractCommandHandler(_idGenerator, _contractRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts([FromQuery] GetAllContractsDto data)
        {
            var contract = await _contractRepository.GetAllContracts(data);
            if (!contract.HasValue())
            {

                return NotFound(new { errorMessage = new ContractNotFoundException().Message });
            }
            var contractList = contract.Unwrap().Select(u => new
            {
                Id = u.GetContractId(),
                NumberContract = u.GetContractNumber(),
                ExpirationDate = u.GetContractExpirationDate(),
                VehicleId = u.GetVehicle(),
                PolicyId = u.GetPolicy(),
                IsActive = u.IsActive()
            }).ToList();
            return Ok(contractList
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(string id)
        {
            var contract = await _contractRepository.GetContractById(new domain.value_objects.ContractId(id));
            if (!contract.HasValue())
            {
                return NotFound(new { errorMessage = new ContractNotFoundException().Message });
            }
            var contractData = new
            {
                Id = contract.Unwrap().GetContractId(),
                NumberContract = contract.Unwrap().GetContractNumber(),
                ExpirationDate = contract.Unwrap().GetContractExpirationDate(),
                Vehicle = contract.Unwrap().GetVehicle(),
                Policy = contract.Unwrap().GetPolicy(),
                IsActive = contract.Unwrap().IsActive()
            };
            return Ok(contractData);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateContractById([FromBody] UpdateContractDto data, string id)
        {
            try
            {
                var validator = new UpdateContractByIdValidator();
                if (!validator.Validate(data).IsValid)
                {
                    var errorMessages = validator.Validate(data).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                if (data.NumberContract == null && data.ExpirationDate == null)
                {
                    return BadRequest(new { errorMessage = "values is required" });
                }
                var service = new UpdateContractByIdCommandHandler(_contractRepository);
                var command = new UpdateContractByIdCommand(id, data.NumberContract, data.ExpirationDate);
                var response = await service.Execute(command);
                return Ok(new { message = "Contract has been updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActivityContractById(string id)
        {
            try
            {
                var contract = await GetContractById(id);
                await _contractRepository.ToggleActivityContractById(new domain.value_objects.ContractId(id));
                return Ok(new { message = "Activity status of contract has been changed" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }



        //vehicle-----------------------------------------------------------------------------------------------------
        [HttpPost("vehicle")]
        public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
        {
            var validator = new CreateVehicleCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateVehicleCommandHandler(_idGenerator, _contractRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }


        [HttpGet("vehicle/vehicle")]
        public async Task<IActionResult> GetCraneById([FromQuery] string contractId, [FromBody] string vehicleId)
        {
            try
            {
                var vehicle = await _contractRepository.GetVehicleById(new GetVehicleByIdDto(contractId, vehicleId));
                if (!vehicle.HasValue())
                {
                    return NotFound(new { errorMessage = new VehicleNotFoundException().Message });
                }
                var data = vehicle.Unwrap();
                return Ok(new
                {
                    Id = data.GetId(),
                    LicensePlate = data.GetLicensePlate(),
                    Brand = data.GetBrand(),
                    Model = data.GetModel(),
                    Year = data.GetYear(),
                    Color = data.GetColor(),
                    Km = data.GetKm(),
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }


        //policy-----------------------------------------------------------------------------------------------------
        [HttpPost("policy")]
        public async Task<IActionResult> CreatePolicy(CreatePolicyCommand command)
        {
            var validator = new CreatePolicyCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreatePolicyCommandHandler(_idGenerator, _contractRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet("policy/policy")]
        public async Task<IActionResult> GetPolicyById([FromQuery] string contractId, [FromBody] string policyId)
        {
            try
            {
                var policy = await _contractRepository.GetPolicyById(new GetPolicyByIdDto(contractId, policyId));
                if (!policy.HasValue())
                {
                    return NotFound(new { errorMessage = new PolicyNotFoundException().Message });
                }
                var data = policy.Unwrap();
                return Ok(new
                {
                    Id = data.GetId(),
                    Name = data.GetName(),
                    MonetaryCoverage = data.GetMonetaryCoverage(),
                    KmCoverage = data.GetkmCoverage(),
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }
    }
}
