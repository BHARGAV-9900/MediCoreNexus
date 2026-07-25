using MediatR;

namespace MediCore.Application.Features.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
}