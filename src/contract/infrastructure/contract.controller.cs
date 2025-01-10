using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.infrastructure.dto;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.infrastructure.validators;
using OrdersMicroservice.src.contract.application.commands.create_contract;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.application.commands.update_contract;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using Microsoft.AspNetCore.Authorization;

namespace OrdersMicroservice.src.contract.infrastructure
{
    [Route("contract")]
    [ApiController]
    [Authorize]
    public class ContractController(IContractRepository contractRepository, 
        IPolicyRepository policyRepository, IIdGenerator<string> idGenerator) : Controller
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPolicyRepository _policyRepository = policyRepository;
        private readonly IIdGenerator<string> _idGenerator = idGenerator;

        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractCommand command)
        {
            var validator = new CreateContractCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateContractCommandHandler(_idGenerator, _contractRepository, _policyRepository);
            var result = await service.Execute(command);
            if (result.IsFailure)
            {
                return BadRequest(new { errors = result.ErrorMessage() });
            }
            var data = result.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts([FromQuery] GetAllContractsDto data)
        {
            var contracts = await _contractRepository.GetAllContracts(data);
            if (!contracts.HasValue())
            {
                return NotFound(new {errorMessage = new NoContractsFoundException().Message });
            }
            var contractsList = contracts.Unwrap()
                .Select(
                    c => new
                    {
                        Id = c.GetId(),
                        NumberContract = c.GetContractNumber(),
                        ExpirationDate = c.GetContractExpirationDate(),
                        Vehicle = new
                        {
                            Id = c.GetVehicle().GetId(),
                            LicensePlate = c.GetVehicle().GetLicensePlate(),
                            Brand = c.GetVehicle().GetBrand(),
                            Model = c.GetVehicle().GetModel(),
                            Year = c.GetVehicle().GetYear(),
                            Color = c.GetVehicle().GetColor(),
                            Km = c.GetVehicle().GetKm(),
                            OwnerDni = c.GetVehicle().GetOwnerDni(),
                            OwnerName = c.GetVehicle().GetOwnerName()
                        },
                        Policy = new
                        {
                            Id = c.GetPolicy().GetId(),
                            Name = c.GetPolicy().GetName(),
                            MonetaryCoverage = c.GetPolicy().GetMonetaryCoverage(),
                            KmCoverage = c.GetPolicy().GetkmCoverage(),
                            BaseKmPrice = c.GetPolicy().GetBaseKmPrice()
                        }
                    }
                ).ToList();
            return Ok(contractsList);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetContractById(string id)
        {
            var contract = await _contractRepository.GetContractById(new ContractId(id));
            if (!contract.HasValue())
            {
                return NotFound(new { errorMessage = new ContractNotFoundException().Message });
            }
            var data = contract.Unwrap();
            return Ok(new
            {
                Id = data.GetId(),
                NumberContract = data.GetContractNumber(),
                ExpirationDate = data.GetContractExpirationDate(),
                Vehicle = new
                {
                    Id = data.GetVehicle().GetId(),
                    LicensePlate = data.GetVehicle().GetLicensePlate(),
                    Brand = data.GetVehicle().GetBrand(),
                    Model = data.GetVehicle().GetModel(),
                    Year = data.GetVehicle().GetYear(),
                    Color = data.GetVehicle().GetColor(),
                    Km = data.GetVehicle().GetKm(),
                    OwnerDni = data.GetVehicle().GetOwnerDni(),
                    OwnerName = data.GetVehicle().GetOwnerName()
                },
                Policy = new
                {
                    Id = data.GetPolicy().GetId(),
                    Name = data.GetPolicy().GetName(),
                    MonetaryCoverage = data.GetPolicy().GetMonetaryCoverage(),
                    KmCoverage = data.GetPolicy().GetkmCoverage(),
                    BaseKmPrice = data.GetPolicy().GetBaseKmPrice()
                }
            });
        }

        [HttpGet("by-number/{number}")]
        public async Task<IActionResult> GetContractByContractNumber(int number)
        {
            var contract = await _contractRepository.GetContractByContractNumber(new NumberContract(number));
            if (!contract.HasValue())
            {
                return NotFound(new { errorMessage = new ContractNotFoundException().Message });
            }
            var data = contract.Unwrap();
            return Ok(new
            {
                Id = data.GetId(),
                NumberContract = data.GetContractNumber(),
                ExpirationDate = data.GetContractExpirationDate(),
                Vehicle = new
                {
                    Id = data.GetVehicle().GetId(),
                    LicensePlate = data.GetVehicle().GetLicensePlate(),
                    Brand = data.GetVehicle().GetBrand(),
                    Model = data.GetVehicle().GetModel(),
                    Year = data.GetVehicle().GetYear(),
                    Color = data.GetVehicle().GetColor(),
                    Km = data.GetVehicle().GetKm(),
                    OwnerDni = data.GetVehicle().GetOwnerDni(),
                    OwnerName = data.GetVehicle().GetOwnerName()
                },
                Policy = new
                {
                    Id = data.GetPolicy().GetId(),
                    Name = data.GetPolicy().GetName(),
                    MonetaryCoverage = data.GetPolicy().GetMonetaryCoverage(),
                    KmCoverage = data.GetPolicy().GetkmCoverage(),
                    BaseKmPrice = data.GetPolicy().GetBaseKmPrice()
                }
            });    
        }

        [HttpGet("vehicle/{id}")]
        public async Task<IActionResult> GetContractVehicleById(string id)
        {
            var vehicle = await _contractRepository.GetContractVehicle(new ContractId(id));
            if (!vehicle.HasValue())
            {
                return NotFound(new { errorMessage = new VehicleNotFoundException().Message });
            }
            var data = vehicle.Unwrap();
            return Ok(new
            {
                id = data.GetId(),
                licensePlate = data.GetLicensePlate(),
                brand = data.GetBrand(),
                model = data.GetModel(),
                year = data.GetYear(),
                color = data.GetColor(),
                km = data.GetKm(),
                ownerDni = data.GetOwnerDni(),
                ownerName = data.GetOwnerName()
            });
        }

        [HttpGet("policy/{id}")]
        public async Task<IActionResult> GetContractPolicyById(string id)
        {
            var policy = await _contractRepository.GetContractPolicy(new ContractId(id));
            if (!policy.HasValue())
            {
                return NotFound(new { errorMessage = new PolicyNotFoundException().Message });
            }
            var data = policy.Unwrap();
            return Ok(new
            {
                id = data.GetId(),
                nameof = data.GetName(),
                monetaryCoverage = data.GetMonetaryCoverage(),
                kmCoverage = data.GetkmCoverage(),
                baseKmPrice = data.GetBaseKmPrice()
            });
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateContractById([FromBody] UpdateContractDto data, string id)
        {
            try
            {
                var service = new UpdateContractByIdCommandHandler(_contractRepository, _policyRepository);
                var command = new UpdateContractCommand(id, data.ExpirationDate, data.PolicyId);
                var response = await service.Execute(command);
                return Ok(new {message = "Contract has been updated" });
            } catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }

        [HttpPatch("status/{id}")]
        public async Task<IActionResult> ToggleActivityContractById(string id)
        {
            try
            {
                await _contractRepository.ToggleActivityContractById(new ContractId(id));
                return Ok(new { message = "Activity status of contract has been changed" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }

    }
}
