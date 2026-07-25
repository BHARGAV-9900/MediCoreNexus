using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Doctor : BaseAuditableEntity
{
    // Required by EF Core
    private Doctor()
    {
    }


    public Doctor(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string specialization,
        int experienceYears,
        decimal consultationFee,
        int departmentId)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);

        PhoneNumber = phoneNumber;
        Specialization = specialization;
        ExperienceYears = experienceYears;
        ConsultationFee = consultationFee;
        DepartmentId = departmentId;

        IsAvailable = true;
    }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string Specialization { get; private set; } = string.Empty;

    public int ExperienceYears { get; private set; }

    public decimal ConsultationFee { get; private set; }

    public bool IsAvailable { get; private set; }

    // Foreign Key
    public int DepartmentId { get; private set; }

    // Navigation Property
    public Department? Department { get; private set; }

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public void SetAvailability(bool available)
    {
        IsAvailable = available;
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

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Email = email.Trim();
    }
    public void Update(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string specialization,
        int experienceYears,
        decimal consultationFee,
        int departmentId)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);

            PhoneNumber = phoneNumber;
            Specialization = specialization;
            ExperienceYears = experienceYears;
            ConsultationFee = consultationFee;
            DepartmentId = departmentId;
        }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}