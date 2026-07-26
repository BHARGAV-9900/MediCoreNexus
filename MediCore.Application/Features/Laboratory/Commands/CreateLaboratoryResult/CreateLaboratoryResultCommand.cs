using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryResult;

public class CreateLaboratoryResultCommand : IRequest<int>
{
    public int LaboratoryOrderId { get; set; }

    public string Result { get; set; } = string.Empty;

    public string? Remarks { get; set; }
}