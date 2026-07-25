using MediatR;

namespace MediCore.Application.Features.Medicines.Queries.GetAllMedicines;

public record GetAllMedicinesQuery
    : IRequest<IEnumerable<MedicineDto>>;