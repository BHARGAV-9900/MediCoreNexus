using MediatR;

namespace MediCore.Application.Features.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
}