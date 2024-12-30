using FluentValidation;
using OrdersMicroservice.src.contract.application.commands.create_vehicle.types;


namespace OrdersMicroservice.src.contract.infrastructure.validators
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.licensePlate)
                .NotEmpty()
                .WithMessage("licensePlate is required")
                .MinimumLength(6)
                .WithMessage("licensePlate must not be less than 6 characters")
                .MaximumLength(7)
                .WithMessage("licensePlate must not exceed 7 characters");

            RuleFor(x => x.brand)
                .NotEmpty()
                .WithMessage("brand is required");

            RuleFor(x => x.model)
                .NotEmpty()
                .WithMessage("model is required");

            RuleFor(x => x.year)
                .GreaterThan(1998)
                .WithMessage("year must be greater than 1998")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("year must not be in the future");

            RuleFor(x => x.color)
                .NotEmpty()
                .WithMessage("color is required");

            RuleFor(x => x.km)
                .GreaterThanOrEqualTo(0)
                .WithMessage("km must be greater than or equal to 0");
        }

    }
}
