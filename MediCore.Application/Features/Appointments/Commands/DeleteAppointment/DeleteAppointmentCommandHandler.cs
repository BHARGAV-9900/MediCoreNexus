using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandHandler
    : IRequestHandler<DeleteAppointmentCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(
        DeleteAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (appointment is null)
            throw new ArgumentException(
                $"Appointment with Id {request.Id} was not found.");

        appointment.Delete();

        await _appointmentRepository.SaveChangesAsync(
            cancellationToken);
    }
}
