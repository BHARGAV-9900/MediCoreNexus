using MediatR;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Appointments.Commands.UpdateAppointmentStatus;

public class UpdateAppointmentStatusCommand : IRequest
{
    public int Id { get; }

    public AppointmentStatus Status { get; }

    public UpdateAppointmentStatusCommand(
        int id,
        AppointmentStatus status)
    {
        Id = id;
        Status = status;
    }
}