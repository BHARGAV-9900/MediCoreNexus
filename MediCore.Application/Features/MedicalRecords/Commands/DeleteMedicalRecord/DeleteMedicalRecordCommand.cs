using MediatR;

namespace MediCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;

public record DeleteMedicalRecordCommand(int Id) : IRequest;