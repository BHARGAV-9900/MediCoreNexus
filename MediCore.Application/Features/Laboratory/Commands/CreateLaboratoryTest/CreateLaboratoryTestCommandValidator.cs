using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryTest;

public class CreateLaboratoryTestCommandValidator
    : AbstractValidator<CreateLaboratoryTestCommand>
{
    public CreateLaboratoryTestCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}