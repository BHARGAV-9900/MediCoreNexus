using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryOrder;

public class UpdateLaboratoryOrderCommandValidator
    : AbstractValidator<UpdateLaboratoryOrderCommand>
{
    public UpdateLaboratoryOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.AppointmentId)
            .GreaterThan(0);

        RuleFor(x => x.LaboratoryTestId)
            .GreaterThan(0);
    }
}