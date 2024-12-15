using FluentValidation;
using OrdersMicroservice.src.extracost.application.commands.create_extracost.types;


namespace OrdersMicroservice.src.extracost.infrastructure.validators
{
    public class CreateExtraCostCommandValidator : AbstractValidator<CreateExtraCostCommand>
    {
        public CreateExtraCostCommandValidator() 
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2)
                .WithMessage("Name must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must  be posite number");




        }
    }
}
