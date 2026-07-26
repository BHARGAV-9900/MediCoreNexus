using FluentValidation;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryOrder;

public class CreateLaboratoryOrderCommandValidator
    : AbstractValidator<CreateLaboratoryOrderCommand>
{
    public CreateLaboratoryOrderCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0);

        RuleFor(x => x.LaboratoryTestId)
            .GreaterThan(0);
    }
}