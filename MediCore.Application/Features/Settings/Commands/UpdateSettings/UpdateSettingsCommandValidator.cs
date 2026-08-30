using FluentValidation;

namespace MediCore.Application.Features.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator
    : AbstractValidator<UpdateSettingsCommand>
{
    private static readonly string[] SupportedCurrencies =
    {
        "INR",
        "USD",
        "EUR",
        "GBP",
        "AED",
        "SAR",
        "CAD",
        "AUD",
        "JPY",
        "CNY",
        "CHF",
        "SGD"
    };

    private static readonly string[] SupportedDateFormats =
    {
        "dd/MM/yyyy",
        "dd-MM-yyyy",
        "yyyy-MM-dd",
        "yyyy/MM/dd",
        "MM/dd/yyyy"
    };

    private static readonly string[] SupportedTimeZones =
    {
        "Asia/Kolkata",
        "Asia/Dubai",
        "Asia/Riyadh",
        "Asia/Singapore",
        "Asia/Tokyo",
        "Asia/Shanghai",
        "UTC",
        "Europe/London",
        "Europe/Paris",
        "America/New_York",
        "America/Chicago",
        "America/Denver",
        "America/Los_Angeles",
        "Australia/Sydney"
    };

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
            .Must(x => SupportedCurrencies.Contains(x))
            .WithMessage("Unsupported currency.");

        RuleFor(x => x.DateFormat)
            .Must(x => SupportedDateFormats.Contains(x))
            .WithMessage("Unsupported date format.");

        RuleFor(x => x.TimeZone)
            .Must(x => SupportedTimeZones.Contains(x))
            .WithMessage("Unsupported time zone.");

        RuleFor(x => x.DefaultAppointmentDuration)
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(480);

        RuleFor(x => x.LowStockThreshold)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpiryWarningDays)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(365);
    }
}