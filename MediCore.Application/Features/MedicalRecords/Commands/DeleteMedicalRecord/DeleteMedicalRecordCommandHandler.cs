using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;

public class DeleteMedicalRecordCommandHandler
    : IRequestHandler<DeleteMedicalRecordCommand>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public DeleteMedicalRecordCommandHandler(
        IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task Handle(
        DeleteMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (record is null)
            throw new ArgumentException(
                $"Medical Record with Id {request.Id} was not found.");

        record.Delete();

        await _medicalRecordRepository.SaveChangesAsync(
            cancellationToken);
    }
}