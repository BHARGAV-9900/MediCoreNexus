namespace MediCore.Application.Features.Settings.Queries.GetSettings;

public class SettingsDto
{
    public int Id { get; set; }

    public string HospitalName { get; set; }
        = string.Empty;

    public string HospitalEmail { get; set; }
        = string.Empty;

    public string HospitalPhone { get; set; }
        = string.Empty;

    public string HospitalAddress { get; set; }
        = string.Empty;

    public string Currency { get; set; }
        = string.Empty;

    public string DateFormat { get; set; }
        = string.Empty;

    public string TimeZone { get; set; }
        = string.Empty;

    public int DefaultAppointmentDuration { get; set; }

    public int LowStockThreshold { get; set; }

    public int ExpiryWarningDays { get; set; }

    public bool EnableNotifications { get; set; }

    public bool EnableAppointmentNotifications { get; set; }

    public bool EnableBillingNotifications { get; set; }

    public bool EnableLaboratoryNotifications { get; set; }
}