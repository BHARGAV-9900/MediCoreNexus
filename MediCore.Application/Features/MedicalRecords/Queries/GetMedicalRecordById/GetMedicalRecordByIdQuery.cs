using MediatR;

namespace MediCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;

public record GetMedicalRecordByIdQuery(int Id)
    : IRequest<MedicalRecordDto>;