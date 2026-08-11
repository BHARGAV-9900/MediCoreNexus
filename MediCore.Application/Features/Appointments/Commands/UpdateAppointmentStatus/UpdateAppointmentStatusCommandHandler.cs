using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Appointments.Commands.UpdateAppointmentStatus;

public class UpdateAppointmentStatusCommandHandler
    : IRequestHandler<UpdateAppointmentStatusCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public UpdateAppointmentStatusCommandHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(
        UpdateAppointmentStatusCommand request,
        CancellationToken cancellationToken)
    {
        var appointment =
            await _appointmentRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (appointment is null)
        {
            throw new NotFoundException(
                $"Appointment with Id {request.Id} was not found.");
        }

        switch (request.Status)
        {
            case AppointmentStatus.Scheduled:

                if (appointment.Status != AppointmentStatus.Scheduled)
                {
                    throw new ConflictException(
                        "Only a new appointment can have Scheduled status.");
                }

                break;


            case AppointmentStatus.CheckedIn:

                appointment.CheckIn();

                break;


            case AppointmentStatus.InProgress:

                appointment.StartConsultation();

                break;


            case AppointmentStatus.Completed:

                appointment.Complete();

                break;


            case AppointmentStatus.Cancelled:

                appointment.Cancel();

                break;


            case AppointmentStatus.NoShow:

                appointment.MarkNoShow();

                break;


            default:

                throw new ConflictException(
                    "Invalid appointment status.");
        }

        await _appointmentRepository.SaveChangesAsync(
            cancellationToken);
    }
}