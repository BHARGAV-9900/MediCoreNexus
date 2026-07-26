namespace MediCore.Application.Features.PrescriptionItems.Queries;

public class PrescriptionItemDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int PrescriptionId { get; set; }

    public Guid PrescriptionPublicId { get; set; }

    public int MedicineId { get; set; }

    public Guid MedicinePublicId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public string Dosage { get; set; } = string.Empty;

    public string Frequency { get; set; } = string.Empty;

    public int DurationInDays { get; set; }

    public int Quantity { get; set; }
}