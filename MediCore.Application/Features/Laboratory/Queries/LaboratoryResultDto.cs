namespace MediCore.Application.Features.Laboratory.Queries;

public class LaboratoryResultDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int LaboratoryOrderId { get; set; }

    public Guid LaboratoryOrderPublicId { get; set; }

    public string Result { get; set; } = string.Empty;

    public string? Remarks { get; set; }
}