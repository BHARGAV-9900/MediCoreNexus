using MediatR;

namespace MediCore.Application.Features.Appointments.Commands.DeleteAppointment;

public record DeleteAppointmentCommand(int Id) : IRequest;