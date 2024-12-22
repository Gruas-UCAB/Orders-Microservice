using FluentValidation;

using OrdersMicroservice.src.contract.infrastructure.dto;

namespace OrdersMicroservice.src.contract.infrastructure.validators
{
    public class UpdateContractByIdValidator : AbstractValidator<UpdateContractDto>
    {
        public UpdateContractByIdValidator()
        {

            RuleFor(x => x.NumberContract)
                .NotNull()
                .WithMessage("Contrac number must not be null")
                .GreaterThan(0)
                .WithMessage("Contrac number must  be posite number");

            RuleFor(x => x.ExpirationDate)
                .NotNull()
                .WithMessage("must not be null");

        }
    }
}
