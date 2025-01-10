using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.src.contract.application.commands.create_policy.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.infrastructure.validators;
using OrdersMicroservice.src.policy.application.commands.create_policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.infrastructure.dto;
using OrdersMicroservice.src.contract.application.commands.update_contract;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using Microsoft.AspNetCore.Authorization;


namespace OrdersMicroservice.src.contract.infrastructure
{
    [Route("policy")]
    [ApiController]
    [Authorize]
    public class PolicyController(IPolicyRepository policyRepository, IIdGenerator<string> idGenerator) : Controller
    {
        private readonly IPolicyRepository _policyRepository = policyRepository;
        private readonly IIdGenerator<string> _idGenerator = idGenerator;

        [HttpPost]
        public async Task<IActionResult> CreatePolicy(CreatePolicyCommand command)
        {
            var validator = new CreatePolicyCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreatePolicyCommandHandler(_idGenerator, _policyRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicies([FromQuery]GetAllPolicesDto data)
        {
            var policies = await _policyRepository.GetAllPolicies(data);

            if (!policies.HasValue())
            {
                return NotFound(
                    new { errorMessage = new NoPoliciesFoundException().Message }
                    );
            }

            var policyList = policies.Unwrap().Select(p => new
            {
                Id = p.GetId(),
                Name = p.GetName(),
                MonetaryCoverage = p.GetMonetaryCoverage(),
                kmCoverage = p.GetkmCoverage(),
                baseKmPrice = p.GetBaseKmPrice(),
            }).ToList();

            return Ok(policyList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(string id)
        {
            var policy = await _policyRepository.GetPolicyById(new PolicyId(id));
            if (!policy.HasValue())
            {
                return NotFound(new { errorMessage = new PolicyNotFoundException().Message });
            }
            var policyData = new
            {
                Id = policy.Unwrap().GetId(),
                Name = policy.Unwrap().GetName(),
                MonetaryCoverage = policy.Unwrap().GetMonetaryCoverage(),
                KmCoverage = policy.Unwrap().GetkmCoverage(),
                BaseKmPrice = policy.Unwrap().GetBaseKmPrice()
            };
            return Ok(policyData);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdatePolicyById([FromBody] UpdatePolicyByIdDto data, string id)
        {
            try
            {
                var service = new UpdatePolicyByIdCommandHandler(_policyRepository);
                var command = new UpdatePolicyByIdCommand(id, data.Name, data.MonetaryCoverage, data.KmCoverage, data.BaseKmPrice);
                var response = await service.Execute(command);
                return Ok(new { message = "Policy has been updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }

        }
    }
}
