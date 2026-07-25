namespace MediCore.Application.Features.Doctors.DTOs;

public class DoctorDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public decimal ConsultationFee { get; set; }

    public bool IsAvailable { get; set; }

    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;
}