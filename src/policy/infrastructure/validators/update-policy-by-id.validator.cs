using FluentValidation;
using OrdersMicroservice.src.policy.application.commands.update_policy.types;
using OrdersMicroservice.src.policy.infrastructure.dto;

namespace OrdersMicroservice.src.policy.infrastructure.validators
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
                .MinimumLength(10)
                .WithMessage("MonetaryCoverage must not be less than 10 characters")
                .MaximumLength(15)
                .WithMessage("MonetaryCoverage must not exceed 15 characters");
        }
    }
}
