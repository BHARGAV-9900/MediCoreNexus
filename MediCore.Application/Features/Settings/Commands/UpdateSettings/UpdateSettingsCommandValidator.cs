using FluentValidation;

namespace MediCore.Application.Features.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator
    : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator()
    {
        RuleFor(x => x.HospitalName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.HospitalEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.HospitalPhone)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.HospitalAddress)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Currency)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.DateFormat)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.TimeZone)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DefaultAppointmentDuration)
            .GreaterThan(0)
            .LessThanOrEqualTo(480);

        RuleFor(x => x.LowStockThreshold)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpiryWarningDays)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(365);
    }
}