using MediatR;

namespace MediCore.Application.Features.Medicines.Queries.GetMedicineById;

public record GetMedicineByIdQuery(int Id)
    : IRequest<MedicineDto>;