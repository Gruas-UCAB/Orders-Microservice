using FluentValidation;
using OrdersMicroservice.src.order.application.commands.create_extra_cost.types;


namespace OrdersMicroservice.src.order.infrastructure.validators
{
    public class CreateExtraCostCommandValidator : AbstractValidator<CreateExtraCostCommand>
    {
        public CreateExtraCostCommandValidator()
        {   RuleFor(x => x.DefaultPrice)
                .NotNull()
                .WithMessage("Price must not be null")
                .GreaterThan(0)
                .WithMessage("Price must  be posite number");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MinimumLength(2)
                .WithMessage("Description must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("Description must not exceed 50 characters");
        }
    }
}
