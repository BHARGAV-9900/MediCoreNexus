using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryResult;

public class CreateLaboratoryResultCommandValidator
    : AbstractValidator<CreateLaboratoryResultCommand>
{
    public CreateLaboratoryResultCommandValidator()
    {
        RuleFor(x => x.LaboratoryOrderId)
            .GreaterThan(0);

        RuleFor(x => x.Result)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Remarks)
            .MaximumLength(2000);
    }
}