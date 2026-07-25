using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Appointments.Queries;

public class AppointmentDto
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string PatientName { get; set; } = string.Empty;

    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = string.Empty;

    public DateTime AppointmentDate { get; set; }

    public AppointmentStatus Status { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string? Notes { get; set; }
}