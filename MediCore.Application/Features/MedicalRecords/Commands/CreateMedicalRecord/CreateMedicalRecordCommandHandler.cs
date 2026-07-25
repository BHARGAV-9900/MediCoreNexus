using MediatR;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandHandler
    : IRequestHandler<CreateMedicalRecordCommand, int>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public CreateMedicalRecordCommandHandler(
        IMedicalRecordRepository medicalRecordRepository,
        IAppointmentRepository appointmentRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<int> Handle(
        CreateMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        // Ensure the appointment exists
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.AppointmentId,
            cancellationToken);

        if (appointment is null)
            throw new ArgumentException(
                $"Appointment with Id {request.AppointmentId} was not found.");

        // Ensure only one medical record per appointment
        var exists = await _medicalRecordRepository.ExistsForAppointmentAsync(
            request.AppointmentId,
            cancellationToken);

        if (exists)
            throw new ArgumentException(
                "A medical record already exists for this appointment.");

        var medicalRecord = new MedicalRecord(
            request.AppointmentId,
            request.Diagnosis,
            request.Symptoms,
            request.ClinicalNotes,
            request.TreatmentPlan,
            request.FollowUpInstructions);

        await _medicalRecordRepository.AddAsync(
            medicalRecord,
            cancellationToken);

        await _medicalRecordRepository.SaveChangesAsync(
            cancellationToken);

        return medicalRecord.Id;
    }
}