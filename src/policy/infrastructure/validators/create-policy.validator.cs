using FluentValidation;
using OrdersMicroservice.src.policy.application.commands.create_policy.types;

namespace OrdersMicroservice.src.policy.infrastructure.validators
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
                .NotEmpty()
                .WithMessage("MonetaryCoverage is required")
                .MinimumLength(10)
                .WithMessage("MonetaryCoverage must not be less than 10 characters")
                .MaximumLength(15)
                .WithMessage("MonetaryCoverage must not exceed 15 characters");

            RuleFor(x => x.KmCoverage)
                .NotEmpty()
                .WithMessage("KmCoverage is required")
                .MinimumLength(2)
                .WithMessage("KmCoverage must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("KmCoverage must not exceed 50 characters");


        }
    }
}
