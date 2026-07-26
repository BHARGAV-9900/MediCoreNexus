using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryTest;

public class UpdateLaboratoryTestCommandValidator
    : AbstractValidator<UpdateLaboratoryTestCommand>
{
    public UpdateLaboratoryTestCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}