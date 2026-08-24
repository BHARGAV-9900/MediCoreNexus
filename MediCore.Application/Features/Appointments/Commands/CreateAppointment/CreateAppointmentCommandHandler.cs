using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly INotificationService _notificationService;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        INotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _notificationService = notificationService;
    }

    public async Task<int> Handle(
        CreateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        // Appointment must be in the future
        if (request.AppointmentDate <= DateTime.UtcNow)
        {
            throw new ConflictException(
                "Appointment date and time must be in the future.");
        }

        // Verify Patient exists
        var patient = await _patientRepository.GetByIdAsync(
            request.PatientId,
            cancellationToken);

        if (patient is null)
            throw new NotFoundException(
                $"Patient with Id {request.PatientId} was not found.");

        // Verify Doctor exists
        var doctor = await _doctorRepository.GetByIdAsync(
            request.DoctorId,
            cancellationToken);

        if (doctor is null)
            throw new NotFoundException(
                $"Doctor with Id {request.DoctorId} was not found.");

        // Prevent duplicate appointment
        var exists = await _appointmentRepository.ExistsAsync(
            request.PatientId,
            request.DoctorId,
            request.AppointmentDate,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                "An appointment already exists for the selected patient, doctor and time.");

        // Check doctor's availability
        var doctorAvailable =
            await _appointmentRepository.IsDoctorAvailableAsync(
                request.DoctorId,
                request.AppointmentDate,
                null,
                cancellationToken);

        if (!doctorAvailable)
            throw new ConflictException(
                "The selected doctor is not available at the requested time.");

        // Check patient's availability
        var patientAvailable =
            await _appointmentRepository.IsPatientAvailableAsync(
                request.PatientId,
                request.AppointmentDate,
                null,
                cancellationToken);

        if (!patientAvailable)
            throw new ConflictException(
                "The patient already has an appointment at the requested time.");

        // Create Appointment using Domain Constructor
        var appointment = new Appointment(
            request.PatientId,
            request.DoctorId,
            request.AppointmentDate,
            request.Reason,
            request.Notes);

        await _appointmentRepository.AddAsync(
            appointment,
            cancellationToken);

        await _appointmentRepository.SaveChangesAsync(
            cancellationToken);

        // Notify administrative/front-desk users about the new appointment.
        // Notifications are stored per user, so each recipient sees the
        // notification only in their own notification inbox.
        await _notificationService.NotifyRolesAsync(
            new[]
            {
                UserRole.Administrator,
                UserRole.Receptionist
            },
            "New Appointment Scheduled",
            $"Appointment scheduled for {patient.FullName} with Dr. {doctor.FirstName} {doctor.LastName} on {request.AppointmentDate:dd-MMM-yyyy hh:mm tt} UTC.",
            "Appointment",
            cancellationToken);

        return appointment.Id;
    }
}