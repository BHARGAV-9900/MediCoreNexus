using FluentValidation;

namespace MediCore.Application.Features.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandValidator
    : AbstractValidator<UpdateMedicineCommand>
{
    public UpdateMedicineCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Manufacturer)
            .MaximumLength(150);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0);
    }
}