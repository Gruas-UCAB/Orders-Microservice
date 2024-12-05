using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.policy.application.commands.create_policy;
using OrdersMicroservice.src.policy.application.commands.create_policy.types;

using OrdersMicroservice.src.policy.application.commands.update_policy;
using OrdersMicroservice.src.policy.application.commands.update_policy.types;
using OrdersMicroservice.src.policy.application.commands.update_user;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.application.repositories.dto;
using OrdersMicroservice.src.policy.application.repositories.exceptions;
using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.policy.infrastructure.dto;
using OrdersMicroservice.src.policy.infrastructure.repositories;
using OrdersMicroservice.src.policy.infrastructure.validators;
using OrdersMicroservice.src.policyt.infrastructure.validators;


namespace OrdersMicroservice.src.policy.infrastructure
{
    [Route("api/policy")]
    [ApiController]
    public class PolicyController : Controller
    {
        private readonly IPolicyRepository _policyRepository = new MongoPolicyRepository();
        
        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreatePolicyCommand command)
        {
            var validator = new CreatePolicyCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreatePolicyCommandHandler(_idGenerator, _policyRepository );
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicies()
        {
            var policies = await _policyRepository.GetAllPolicies();

            if (!policies.HasValue())
            {
                return NotFound(
                    new { errorMessage = new PolicyNotFoundException().Message }
                    );
            }

            var policyList = policies.Unwrap().Select(d => new
            {
                Id = d.GetId(),
                Name = d.GetName(),
                MonetaryCoverage = d.GetMonetaryCoverage(),
                kmCoverage = d.GetkmCoverage()
            }).ToList();

            return Ok(policyList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(string id)
        {
            var policy = await _policyRepository.GetPolicyById(new domain.value_objects.PolicyId(id));
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
            };
            return Ok(policyData);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdatePolicyById([FromBody] UpdatePolicyDto data, string id)
        {
            try
            {
                var validator = new UpdatePolicyByIdValidator();
                if (!validator.Validate(data).IsValid)
                {
                    var errorMessages = validator.Validate(data).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                if (data.Name == null && data.MonetaryCoverage == null && data.KmCoverage == null)
                {
                    return BadRequest(new { errorMessage = "values is required" });
                }
                var service = new UpdatePolicyByIdCommandHandler(_policyRepository);
                var command = new UpdatePolicyByIdCommand(id, data.Name, data.MonetaryCoverage, data.KmCoverage);
                var response = await service.Execute(command);
                return Ok(new { message = "User has been updated"});
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActivityPolicyById(string id)
        {
            try
            {
                var user = await GetPolicyById(id);
                await _policyRepository.ToggleActivityPolicyById(new domain.value_objects.PolicyId(id));
                return Ok(new { message = "Activity status of user has been changed" });
            } catch( Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }
    }
}
