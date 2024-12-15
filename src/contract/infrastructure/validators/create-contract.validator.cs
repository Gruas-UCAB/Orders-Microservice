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
                .WithMessage("MonetaryCoverage must not be null")
                .GreaterThan(0)
                .WithMessage("MonetaryCoverage must  be posite number");


            RuleFor(x => x.ContractExpirationDate);



            RuleFor(x => x.VehicleId)
                .NotNull()
                .WithMessage("must not be null");
 


            RuleFor(x => x.PolicyId)
                .NotNull()
                .WithMessage("must not be null");
     
        }
    }
}
