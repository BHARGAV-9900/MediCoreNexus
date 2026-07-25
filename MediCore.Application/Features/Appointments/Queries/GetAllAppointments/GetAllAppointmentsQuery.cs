using MediatR;

namespace MediCore.Application.Features.Appointments.Queries.GetAllAppointments;

public record GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>;