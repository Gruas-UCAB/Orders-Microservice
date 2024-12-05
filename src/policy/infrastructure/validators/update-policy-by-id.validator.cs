using FluentValidation;
using OrdersMicroservice.src.policy.application.commands.update_policy.types;
using OrdersMicroservice.src.policy.infrastructure.dto;

namespace OrdersMicroservice.src.policyt.infrastructure.validators
{
    public class UpdatePolicyByIdValidator : AbstractValidator<UpdatePolicyDto>
    {
        public UpdatePolicyByIdValidator()
        {

            RuleFor(x => x.Name)
                .MinimumLength(2)
                .WithMessage("Name must not be less than 2 characters")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.MonetaryCoverage)
                .NotNull()
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must not be posite number");
        }
    }
}
