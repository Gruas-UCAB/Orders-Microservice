using FluentValidation;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;

namespace OrdersMicroservice.src.contract.infrastructure.validators
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator() 
        {
            RuleFor(x => x.ContractNumber)
            .NotNull()
            .WithMessage("Contact number must not be null")
            .GreaterThan(0)
            .WithMessage("Contract number must  be posite number");


            RuleFor(x => x.ContractExpirationDate)
            .NotNull()
            .WithMessage("must not be null");


            /*RuleFor(x => x.Vehicle)
                .NotNull()
                .WithMessage("must not be null");*/
 


            RuleFor(x => x.PolicyId)
                .NotNull()
                .WithMessage("must not be null");
     
        }
    }
}
