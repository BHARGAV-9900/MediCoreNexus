using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandHandler
    : IRequestHandler<UpdateAppointmentCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly INotificationService _notificationService;

    public UpdateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        INotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _notificationService = notificationService;
    }

    public async Task Handle(
        UpdateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment =
            await _appointmentRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                $"Appointment with Id {request.Id} was not found.");

        var oldAppointmentDate = appointment.AppointmentDate;
        var oldReason = appointment.Reason;
        var oldNotes = appointment.Notes;

        // Only validate date/time when appointment time changes
        if (appointment.AppointmentDate != request.AppointmentDate)
        {
            // New appointment time must be in the future
            if (request.AppointmentDate <= DateTime.UtcNow)
            {
                throw new ConflictException(
                    "Appointment date and time must be in the future.");
            }

            // Check doctor availability
            var doctorAvailable =
                await _appointmentRepository.IsDoctorAvailableAsync(
                    appointment.DoctorId,
                    request.AppointmentDate,
                    appointment.Id,
                    cancellationToken);

            if (!doctorAvailable)
                throw new ConflictException(
                    "The selected doctor is not available at the requested time.");

            // Check patient availability
            var patientAvailable =
                await _appointmentRepository.IsPatientAvailableAsync(
                    appointment.PatientId,
                    request.AppointmentDate,
                    appointment.Id,
                    cancellationToken);

            if (!patientAvailable)
                throw new ConflictException(
                    "The patient already has an appointment at the requested time.");
        }

        appointment.Update(
            request.AppointmentDate,
            request.Reason,
            request.Notes);

        await _appointmentRepository.SaveChangesAsync(
            cancellationToken);

        var patientName = appointment.Patient is not null
            ? $"{appointment.Patient.FirstName} {appointment.Patient.LastName}"
            : $"Patient #{appointment.PatientId}";

        var doctorName = appointment.Doctor is not null
            ? $"Dr. {appointment.Doctor.FirstName} {appointment.Doctor.LastName}"
            : $"Doctor #{appointment.DoctorId}";

        var changes = new List<string>();

        if (oldAppointmentDate != appointment.AppointmentDate)
        {
            changes.Add(
                $"date/time changed from {oldAppointmentDate:dd-MMM-yyyy hh:mm tt} UTC to {appointment.AppointmentDate:dd-MMM-yyyy hh:mm tt} UTC");
        }

        if (!string.Equals(oldReason, appointment.Reason, StringComparison.Ordinal))
            changes.Add("reason updated");

        if (!string.Equals(oldNotes, appointment.Notes, StringComparison.Ordinal))
            changes.Add("notes updated");

        await _notificationService.NotifyRolesAsync(
            new[]
            {
                MediCore.Domain.Enums.UserRole.Administrator,
                MediCore.Domain.Enums.UserRole.Receptionist
            },
            "Appointment Updated",
            $"Appointment for {patientName} with {doctorName} was updated. {string.Join("; ", changes)}.",
            "Appointment",
            cancellationToken);
    }
}
