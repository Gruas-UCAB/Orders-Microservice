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
            .GreaterThan(1000)
            .WithMessage("Contract number must  be posite number higher than 1000");

            RuleFor(x => x.ContractExpirationDate)
            .NotNull()
            .WithMessage("must not be null");
 
            RuleFor(x => x.LicensePlate)
            .NotNull()
            .WithMessage("Vehicle license plate must not be null");

            RuleFor(x => x.Brand)
                .NotNull()
                .WithMessage("Vehicle brand must not be null");

            RuleFor(x => x.Model)
                .NotNull()
                .WithMessage("Vehicle model must not be null");

            RuleFor(x => x.Year)
                .NotNull()
                .WithMessage("Vehicle year must not be null");
                
            RuleFor(x => x.OwnerDni)
                .NotNull()
                .WithMessage("Owner DNI must not be null");

            RuleFor(x => x.OwnerName)
                .NotNull()
                .WithMessage("Owner name must not be null");

            RuleFor(x => x.PolicyId)
                .NotNull()
                .WithMessage("must not be null");
     
        }
    }
}
