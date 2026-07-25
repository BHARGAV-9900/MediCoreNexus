using MediatR;
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
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (appointment is null)
            throw new ArgumentException(
                $"Appointment with Id {request.Id} was not found.");

        // Check availability only if the appointment time changes
        if (appointment.AppointmentDate != request.AppointmentDate)
        {
            var doctorAvailable =
                await _appointmentRepository.IsDoctorAvailableAsync(
                    appointment.DoctorId,
                    request.AppointmentDate,
                    appointment.Id,
                    cancellationToken);

            if (!doctorAvailable)
                throw new ArgumentException(
                    "The selected doctor is not available at the requested time.");

            var patientAvailable =
                await _appointmentRepository.IsPatientAvailableAsync(
                    appointment.PatientId,
                    request.AppointmentDate,
                    appointment.Id,
                    cancellationToken);

            if (!patientAvailable)
                throw new ArgumentException(
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