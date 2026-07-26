using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommandHandler
    : IRequestHandler<UpdateMedicalRecordCommand>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public UpdateMedicalRecordCommandHandler(
        IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task Handle(
        UpdateMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (record is null)
            throw new NotFoundException(
                $"Medical Record with Id {request.Id} was not found.");

        record.Update(
            request.Diagnosis,
            request.Symptoms,
            request.ClinicalNotes,
            request.TreatmentPlan,
            request.FollowUpInstructions);

        await _medicalRecordRepository.SaveChangesAsync(
            cancellationToken);
    }
}