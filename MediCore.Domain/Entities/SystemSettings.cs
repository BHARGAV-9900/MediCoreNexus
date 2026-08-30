using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class SystemSettings : BaseAuditableEntity
{
    private SystemSettings()
    {
    }

    public SystemSettings(
        string hospitalName,
        string hospitalEmail,
        string hospitalPhone,
        string hospitalAddress,
        string currency,
        string dateFormat,
        string timeZone,
        int defaultAppointmentDuration,
        int lowStockThreshold,
        int expiryWarningDays)
    {
        SetHospitalName(hospitalName);
        SetHospitalEmail(hospitalEmail);
        SetHospitalPhone(hospitalPhone);
        SetHospitalAddress(hospitalAddress);

        Currency = currency;
        DateFormat = dateFormat;
        TimeZone = timeZone;

        DefaultAppointmentDuration = defaultAppointmentDuration;
        LowStockThreshold = lowStockThreshold;
        ExpiryWarningDays = expiryWarningDays;

        EnableNotifications = true;
        EnableAppointmentNotifications = true;
        EnableBillingNotifications = true;
        EnableLaboratoryNotifications = true;
    }

    public string HospitalName { get; private set; } = string.Empty;
    public string HospitalEmail { get; private set; } = string.Empty;
    public string HospitalPhone { get; private set; } = string.Empty;
    public string HospitalAddress { get; private set; } = string.Empty;
    public string Currency { get; private set; } = "INR";
    public string DateFormat { get; private set; } = "dd/MM/yyyy";
    public string TimeZone { get; private set; } = "Asia/Kolkata";

    public int DefaultAppointmentDuration { get; private set; }
    public int LowStockThreshold { get; private set; }
    public int ExpiryWarningDays { get; private set; }
    public bool EnableNotifications { get; private set; }
    public bool EnableAppointmentNotifications { get; private set; }
    public bool EnableBillingNotifications { get; private set; }
    public bool EnableLaboratoryNotifications { get; private set; }

    public void Update(
        string hospitalName,
        string hospitalEmail,
        string hospitalPhone,
        string hospitalAddress,
        string currency,
        string dateFormat,
        string timeZone,
        int defaultAppointmentDuration,
        int lowStockThreshold,
        int expiryWarningDays,
        bool enableNotifications,
        bool enableAppointmentNotifications,
        bool enableBillingNotifications,
        bool enableLaboratoryNotifications)
    {
        SetHospitalName(hospitalName);
        SetHospitalEmail(hospitalEmail);
        SetHospitalPhone(hospitalPhone);
        SetHospitalAddress(hospitalAddress);

        Currency = currency;
        DateFormat = dateFormat;
        TimeZone = timeZone;
        DefaultAppointmentDuration = defaultAppointmentDuration;
        LowStockThreshold = lowStockThreshold;
        ExpiryWarningDays = expiryWarningDays;
        EnableNotifications = enableNotifications;
        EnableAppointmentNotifications = enableAppointmentNotifications;
        EnableBillingNotifications = enableBillingNotifications;
        EnableLaboratoryNotifications = enableLaboratoryNotifications;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetHospitalName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hospital name is required.");
        HospitalName = value.Trim();
    }

    private void SetHospitalEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hospital email is required.");
        HospitalEmail = value.Trim().ToLowerInvariant();
    }

    private void SetHospitalPhone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hospital phone is required.");
        HospitalPhone = value.Trim();
    }

    private void SetHospitalAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hospital address is required.");
        HospitalAddress = value.Trim();
    }
}