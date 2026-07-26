using MediatR;

namespace MediCore.Application.Features.PrescriptionItems.Commands.CreatePrescriptionItem;

public class CreatePrescriptionItemCommand : IRequest<int>
{
    public int PrescriptionId { get; set; }

    public int MedicineId { get; set; }

    public string Dosage { get; set; } = string.Empty;

    public string Frequency { get; set; } = string.Empty;

    public int DurationInDays { get; set; }

    public int Quantity { get; set; }
}