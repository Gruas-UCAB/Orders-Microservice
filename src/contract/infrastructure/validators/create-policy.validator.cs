using FluentValidation;
using OrdersMicroservice.src.contract.application.commands.create_policy.types;


namespace OrdersMicroservice.src.contract.infrastructure.validators
{
    public class CreatePolicyCommandValidator : AbstractValidator<CreatePolicyCommand>
    {
        public CreatePolicyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2)
                .WithMessage("Name must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.MonetaryCoverage)
                .NotNull()
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must  be posite number");

            RuleFor(x => x.KmCoverage)
                .NotNull()
                .WithMessage("KmCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("KmCoverage must  be posite number");


        }
    }
}
