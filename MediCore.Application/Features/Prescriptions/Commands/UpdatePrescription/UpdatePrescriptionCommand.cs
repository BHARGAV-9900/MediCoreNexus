using MediatR;

namespace MediCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string Instructions { get; set; } = string.Empty;

    public string? Notes { get; set; }
}