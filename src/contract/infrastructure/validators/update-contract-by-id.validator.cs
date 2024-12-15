using FluentValidation;

using OrdersMicroservice.src.contract.infrastructure.dto;

namespace UsersMicroservice.src.contract.infrastructure.validators
{
    public class UpdateContractByIdValidator : AbstractValidator<UpdateContractDto>
    {
        public UpdateContractByIdValidator()
        {

            RuleFor(x => x.NumberContract)
                .NotNull()
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must  be posite number");

            RuleFor(x => x.ExpirationDate);

 
        }
    }
}
