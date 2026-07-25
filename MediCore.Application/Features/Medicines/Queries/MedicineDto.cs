namespace MediCore.Application.Features.Medicines.Queries;

public class MedicineDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; }
}