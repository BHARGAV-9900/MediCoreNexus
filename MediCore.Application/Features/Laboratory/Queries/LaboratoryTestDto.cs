namespace MediCore.Application.Features.Laboratory.Queries;

public class LaboratoryTestDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}