using FluentValidation;

namespace MediCore.Application.Features.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandValidator
    : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Manufacturer)
            .MaximumLength(150);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0);
    }
}