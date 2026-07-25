using MediatR;

namespace MediCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;

public record GetAllMedicalRecordsQuery
    : IRequest<IEnumerable<MedicalRecordDto>>;