using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryResult;

public class UpdateLaboratoryResultCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string Result { get; set; } = string.Empty;

    public string? Remarks { get; set; }
}