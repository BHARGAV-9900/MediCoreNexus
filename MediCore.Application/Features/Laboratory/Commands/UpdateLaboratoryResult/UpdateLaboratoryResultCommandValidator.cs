using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryResult;

public class UpdateLaboratoryResultCommandValidator
    : AbstractValidator<UpdateLaboratoryResultCommand>
{
    public UpdateLaboratoryResultCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Result)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Remarks)
            .MaximumLength(2000);
    }
}