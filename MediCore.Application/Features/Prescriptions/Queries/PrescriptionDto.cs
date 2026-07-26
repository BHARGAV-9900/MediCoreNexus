namespace MediCore.Application.Features.Prescriptions.Queries;

public class PrescriptionDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int AppointmentId { get; set; }

    public Guid AppointmentPublicId { get; set; }

    public string Instructions { get; set; } = string.Empty;

    public string? Notes { get; set; }
}