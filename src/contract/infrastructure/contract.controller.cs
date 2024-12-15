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
using UsersMicroservice.src.contract.application.repositories.dto;

using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.infrastructure.repositories;
using OrdersMicroservice.src.contract.domain.value_objects;
using UsersMicroservice.src.contract.application.commands.update_contract.types;
using UsersMicroservice.src.contract.infrastructure.validators;
using contractsMicroservice.src.contract.infrastructure.repositories;

namespace OrdersMicroservice.src.contract.infrastructure
{
    [Route("api/contract")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IContractRepository _contractRepository = new MongocontractRepository();
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
            var service = new CreateContractCommandHandler(_idGenerator, _contractRepository,_vehicleRepository, _policyRepository);
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
            if ( contract.HasValue())
            {
                return NotFound(new { errorMessage = new ContractNotFoundException().Message });
            }
            var contractList = contract.Unwrap().Select(u => new
            {
                Id = u.GetContractId(),
                NumberContract = u.GetContractNumber(),
                ExpirationDate = u.GetContractExpirationDate(),
                Vehicle = u.GetVehicleId(),
                Policy = u.GetPolicyId(),
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
                Vehicle = contract.Unwrap().GetVehicleId(),
                Policy = contract.Unwrap().GetPolicyId(),
                IsActive = contract.Unwrap().IsActive()
            };
            return Ok(contractData);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateUserById([FromBody] UpdateContractDto data, string id)
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
                return Ok(new { message = "Contract has been updated"});
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
            } catch( Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }
    }
}
