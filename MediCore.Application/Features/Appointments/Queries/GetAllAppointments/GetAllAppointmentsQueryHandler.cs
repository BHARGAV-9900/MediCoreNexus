using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Appointments.Queries.GetAllAppointments;

public class GetAllAppointmentsQueryHandler
    : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAllAppointmentsQueryHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(
        GetAllAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAllAsync(
            cancellationToken);

        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,

            PatientId = a.PatientId,
            PatientName = a.Patient?.FirstName + " " + a.Patient?.LastName,

            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.FirstName + " " + a.Doctor?.LastName,

            AppointmentDate = a.AppointmentDate,

            Status = a.Status,

            Reason = a.Reason,

            Notes = a.Notes
        });
    }
}