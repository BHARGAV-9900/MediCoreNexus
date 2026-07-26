using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(
        CreateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
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
        var doctorAvailable = await _appointmentRepository.IsDoctorAvailableAsync(
            request.DoctorId,
            request.AppointmentDate,
            null,
            cancellationToken);

        if (!doctorAvailable)
            throw new ConflictException(
                "The selected doctor is not available at the requested time.");

        // Check patient's availability
        var patientAvailable = await _appointmentRepository.IsPatientAvailableAsync(
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

        return appointment.Id;
    }
}