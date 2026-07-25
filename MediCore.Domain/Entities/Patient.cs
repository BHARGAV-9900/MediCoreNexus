using MediCore.Domain.Common;
using MediCore.Domain.Enums;

namespace MediCore.Domain.Entities;

public class Patient : BaseAuditableEntity
{
    // Required by Entity Framework Core
    private Patient()
    {
    }

    public Patient(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        Gender gender,
        BloodGroup bloodGroup,
        string phoneNumber,
        string email,
        string address,
        string emergencyContactName,
        string emergencyContactPhone,
        string? insuranceNumber = null)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetDateOfBirth(dateOfBirth);
        SetPhoneNumber(phoneNumber);
        SetEmail(email);

        Gender = gender;
        BloodGroup = bloodGroup;
        Address = address;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone = emergencyContactPhone;
        InsuranceNumber = insuranceNumber;

        IsActive = true;
    }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public DateTime DateOfBirth { get; private set; }

    public Gender Gender { get; private set; }

    public BloodGroup BloodGroup { get; private set; }

    public string PhoneNumber { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string EmergencyContactName { get; private set; } = string.Empty;

    public string EmergencyContactPhone { get; private set; } = string.Empty;

    public string? InsuranceNumber { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Property (Add later after Appointment entity is created)
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public void ChangePhoneNumber(string phoneNumber)
    {
        SetPhoneNumber(phoneNumber);
    }

    public void ChangeEmail(string email)
    {
        SetEmail(email);
    }

    public void ChangeAddress(string address)
    {
        Address = address;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        FirstName = firstName.Trim();
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        LastName = lastName.Trim();
    }

    private void SetDateOfBirth(DateTime dateOfBirth)
    {
        if (dateOfBirth > DateTime.Today)
            throw new ArgumentException("Date of birth cannot be in the future.");

        DateOfBirth = dateOfBirth;
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");

        PhoneNumber = phoneNumber.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Email = email.Trim();
    }
}