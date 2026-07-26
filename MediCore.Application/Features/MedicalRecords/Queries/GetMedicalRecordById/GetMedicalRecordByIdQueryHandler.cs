using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;

public class GetMedicalRecordByIdQueryHandler
    : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordDto>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public GetMedicalRecordByIdQueryHandler(
        IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task<MedicalRecordDto> Handle(
        GetMedicalRecordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (record is null)
            throw new NotFoundException(
                $"Medical Record with Id {request.Id} was not found.");

        return new MedicalRecordDto
        {
            Id = record.Id,
            AppointmentId = record.AppointmentId,
            AppointmentPublicId = record.Appointment!.PublicId,
            Diagnosis = record.Diagnosis,
            Symptoms = record.Symptoms,
            ClinicalNotes = record.ClinicalNotes,
            TreatmentPlan = record.TreatmentPlan,
            FollowUpInstructions = record.FollowUpInstructions
        };
    }
}