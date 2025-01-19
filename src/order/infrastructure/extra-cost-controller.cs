using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.create_extra_cost;
using OrdersMicroservice.src.order.application.commands.create_extra_cost.types;
using OrdersMicroservice.src.order.application.commands.update_extra_cost;
using OrdersMicroservice.src.order.application.commands.update_extra_cost.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.infrastructure.dto;
using OrdersMicroservice.src.order.infrastructure.validators;

namespace OrdersMicroservice.src.order.infrastructure
{
    [Route("extra-cost")]
    [ApiController]
    [Authorize]
    public class ExtraCostController(IExtraCostRepository extraCostRepository, IIdGenerator<string> idGenerator) : Controller
    {
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        private readonly IIdGenerator<string> _idGenerator = idGenerator;

        [HttpPost]
        public async Task<IActionResult> CreateExtraCost(CreateExtraCostCommand command)
        {
            var validator = new CreateExtraCostCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateExtraCostCommandHandler(_extraCostRepository, _idGenerator);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExtraCosts([FromQuery] GetAllExtraCostsDto data)
        {
            var extraCosts = await _extraCostRepository.GetAllExtraCosts(data);

            if (!extraCosts.HasValue())
            {
                return NotFound(
                    new { errorMessage = new NoExtraCostFoundException().Message }
                    );
            }

            var extraCostList = extraCosts.Unwrap().Select(p => new
            {
                Id = p.GetId(),
                Description = p.GetDescription(),
                Price = p.GetPrice()
            }).ToList();

            return Ok(extraCostList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExtraCostById(string id)
        {
            var extraCost = await _extraCostRepository.GetExtraCostById(new ExtraCostId(id));

            if (!extraCost.HasValue())
            {
                return NotFound(
                    new { errorMessage = new ExtraCostNotFoundException().Message }
                    );
            }

            return Ok(new
            {
                Id = extraCost.Unwrap().GetId(),
                Description = extraCost.Unwrap().GetDescription(),
                Price = extraCost.Unwrap().GetPrice()
            });
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateExtraCost([FromBody] UpdateExtraCostDto data, string id)
        {
            if (data.DefaultPrice == null && data.Description == null)
                return BadRequest(new { errorMessage = "defaultPrice and description can not be null at the same time" });
            var service = new UpdateExtraCostCommandHandler(_extraCostRepository);
            var response = await service.Execute(new UpdateExtraCostCommand(id, data.Description, data.DefaultPrice));
            if (response.IsFailure)
                return BadRequest(new { errorMessage = response.ErrorMessage()});
            var extraCostData = response.Unwrap();
            return Ok("The extra cost has been updated succesfully");
        }
    }
}
