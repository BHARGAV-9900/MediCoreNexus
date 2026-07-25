using MediatR;

namespace MediCore.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommand : IRequest
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string? Notes { get; set; }
}