
/*using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.extracost.application.commands.create_extracost;
using OrdersMicroservice.src.extracost.application.commands.create_extracost.types;
using OrdersMicroservice.src.extracost.application.commands.extracost;
using OrdersMicroservice.src.extracost.application.commands.update_extracos.types;
using OrdersMicroservice.src.extracost.application.commands.update_extracost;
using OrdersMicroservice.src.extracost.application.commands.update_extracost.types;

using OrdersMicroservice.src.extracost.application.repositories;
using OrdersMicroservice.src.extracost.application.repositories.dto;
using OrdersMicroservice.src.extracost.application.repositories.exceptions;
using OrdersMicroservice.src.extracost.domain;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.extracost.infrastructure.dto;
using OrdersMicroservice.src.extracost.infrastructure.repositories;
using OrdersMicroservice.src.extracost.infrastructure.validators;



namespace OrdersMicroservice.src.extracost.infrastructure
{
    [Route("api/extraCost")]
    [ApiController]
    public class ExtraCostController : Controller
    {
        private readonly IExtraCostRepository _extraCostRepository = new MongoExtraCostRepository();
        
        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateExtraCostCommand command)
        {
            var validator = new CreateExtraCostCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateExtraCostCommandHandler(_idGenerator, _extraCostRepository );
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExtraCosts()
        {
            var extracosts = await _extraCostRepository.GetAllExtraCosts();

            if (!extracosts.HasValue())
            {
                return NotFound(
                    new { errorMessage = new ExtraCostNotFoundException().Message }
                    );
            }

            var extraCostList = extracosts.Unwrap().Select(d => new
            {
                Id = d.GetExtraCostId(),
                Description = d.GetExtraCostDescription(),
                Price = d.GetExtraCostPrice(),
                
            }).ToList();

            return Ok(extraCostList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExtraCostById(string id)
        {
            var extraCost = await _extraCostRepository.GetExtraCostById(new domain.value_objects.ExtraCostId(id));
            if (!extraCost.HasValue())
            {
                return NotFound(new { errorMessage = new ExtraCostNotFoundException().Message });
            }
            var extraCostData = new
            {
                Id = extraCost.Unwrap().GetExtraCostId(),
                Description = extraCost.Unwrap().GetExtraCostDescription(),
                Price = extraCost.Unwrap().GetExtraCostPrice(),
              
            };
            return Ok(extraCostData);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateExtraCostById([FromBody] UpdateExtraCostDto data, string id)
        {
            try
            {
                var validator = new UpdateExtraCostByIdValidator();
                if (!validator.Validate(data).IsValid)
                {
                    var errorMessages = validator.Validate(data).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                if (data.Description == null && data.Price == null )
                {
                    return BadRequest(new { errorMessage = "values is required" });
                }
                var service = new UpdateExtraCostByIdCommandHandler(_extraCostRepository);
                var command = new UpdateExtraCostByIdCommand(id, data.Description, data.Price);
                var response = await service.Execute(command);
                return Ok(new { message = "User has been updated"});
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActivityExtraCostById(string id)
        {
            try
            {
                var user = await GetExtraCostById(id);
                await _extraCostRepository.ToggleActivityExtraCostById(new domain.value_objects.ExtraCostId(id));
                return Ok(new { message = "Activity status of user has been changed" });
            } catch( Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }
    }
}
*/