using MediatR;

namespace MediCore.Application.Features.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommand : IRequest<int>
{
    public int AppointmentId { get; set; }

    public string Instructions { get; set; } = string.Empty;

    public string? Notes { get; set; }
}