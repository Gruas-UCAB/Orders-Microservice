using FluentValidation;
using OrdersMicroservice.src.extracost.application.commands.update_extracost.types;
using OrdersMicroservice.src.extracost.infrastructure.dto;

namespace OrdersMicroservice.src.extracost.infrastructure.validators
{
    public class UpdateExtraCostByIdValidator : AbstractValidator<UpdateExtraCostDto>
    {
        public UpdateExtraCostByIdValidator()
        {

            RuleFor(x => x.Description)
                .MinimumLength(2)
                .WithMessage("Name must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must not be posite number");
        }
    }
}
