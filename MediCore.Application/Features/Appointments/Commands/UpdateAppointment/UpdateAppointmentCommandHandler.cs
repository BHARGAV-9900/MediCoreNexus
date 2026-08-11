using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandHandler
    : IRequestHandler<UpdateAppointmentCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public UpdateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
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
    }
}