namespace MediCore.Application.Features.Laboratory.Queries;

public class LaboratoryOrderDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    // Appointment
    public int AppointmentId { get; set; }

    public Guid AppointmentPublicId { get; set; }

    public DateTime AppointmentDate { get; set; }

    // Patient
    public int PatientId { get; set; }

    public string PatientName { get; set; } = string.Empty;

    // Doctor
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = string.Empty;

    // Laboratory Test
    public int LaboratoryTestId { get; set; }

    public Guid LaboratoryTestPublicId { get; set; }

    public string LaboratoryTestName { get; set; } = string.Empty;

    public decimal LaboratoryTestPrice { get; set; }
}