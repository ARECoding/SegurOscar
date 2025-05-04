using SegurOsCar.DTOs;
using FluentValidation;
namespace SegurOsCar.Validators
{
    public class VehicleInsertValidator : AbstractValidator<VehicleInsertDto>
    {
        public VehicleInsertValidator()
        {
            RuleFor(vehicle => vehicle.LicencePlate)
                .NotEmpty()
                .WithMessage("The licence plate is required.")
                .Matches(@"^[A-Z]{3}-\d{4}$");
        }
    }
}
