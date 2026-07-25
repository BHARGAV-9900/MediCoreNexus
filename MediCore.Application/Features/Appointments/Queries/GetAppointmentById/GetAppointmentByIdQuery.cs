using MediatR;

namespace MediCore.Application.Features.Appointments.Queries.GetAppointmentById;

public record GetAppointmentByIdQuery(int Id)
    : IRequest<AppointmentDto>;