using MediatR;

namespace MediCore.Application.Features.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommand
    : IRequest<bool>
{
    public string HospitalName { get; set; }
        = string.Empty;

    public string HospitalEmail { get; set; }
        = string.Empty;

    public string HospitalPhone { get; set; }
        = string.Empty;

    public string HospitalAddress { get; set; }
        = string.Empty;

    public string Currency { get; set; }
        = "INR";

    public string DateFormat { get; set; }
        = "dd-MMM-yyyy";

    public string TimeZone { get; set; }
        = "Asia/Kolkata";

    public int DefaultAppointmentDuration { get; set; }

    public int LowStockThreshold { get; set; }

    public int ExpiryWarningDays { get; set; }

    public bool EnableNotifications { get; set; }

    public bool EnableAppointmentNotifications { get; set; }

    public bool EnableBillingNotifications { get; set; }

    public bool EnableLaboratoryNotifications { get; set; }
}