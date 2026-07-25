using MediatR;

namespace MediCore.Application.Features.Medicines.Commands.DeleteMedicine;

public record DeleteMedicineCommand(int Id) : IRequest<bool>;