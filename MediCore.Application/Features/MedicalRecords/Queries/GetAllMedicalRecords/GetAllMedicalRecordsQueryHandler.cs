using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;

public class GetAllMedicalRecordsQueryHandler
    : IRequestHandler<GetAllMedicalRecordsQuery, IEnumerable<MedicalRecordDto>>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public GetAllMedicalRecordsQueryHandler(
        IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task<IEnumerable<MedicalRecordDto>> Handle(
        GetAllMedicalRecordsQuery request,
        CancellationToken cancellationToken)
    {
        var medicalRecords = await _medicalRecordRepository.GetAllAsync(
            cancellationToken);

        return medicalRecords.Select(record => new MedicalRecordDto
        {
            Id = record.Id,
            AppointmentId = record.AppointmentId,
            AppointmentPublicId = record.Appointment!.PublicId,
            Diagnosis = record.Diagnosis,
            Symptoms = record.Symptoms,
            ClinicalNotes = record.ClinicalNotes,
            TreatmentPlan = record.TreatmentPlan,
            FollowUpInstructions = record.FollowUpInstructions
        });
    }
}