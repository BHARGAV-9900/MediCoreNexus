using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler
    : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentByIdQueryHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<AppointmentDto> Handle(
        GetAppointmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                $"Appointment with Id {request.Id} was not found.");

        return new AppointmentDto
        {
            Id = appointment.Id,

            PatientId = appointment.PatientId,
            PatientName = $"{appointment.Patient?.FirstName} {appointment.Patient?.LastName}",

            DoctorId = appointment.DoctorId,
            DoctorName = $"{appointment.Doctor?.FirstName} {appointment.Doctor?.LastName}",

            AppointmentDate = appointment.AppointmentDate,

            Status = appointment.Status,

            Reason = appointment.Reason,

            Notes = appointment.Notes
        };
    }
}