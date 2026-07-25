using MediatR;

namespace MediCore.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<int>
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string? Notes { get; set; }
}