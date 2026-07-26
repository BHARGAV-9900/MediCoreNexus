namespace MediCore.Application.Features.Laboratory.Queries;

public class LaboratoryOrderDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int AppointmentId { get; set; }

    public Guid AppointmentPublicId { get; set; }

    public int LaboratoryTestId { get; set; }

    public Guid LaboratoryTestPublicId { get; set; }
}