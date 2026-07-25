using MediatR;

namespace MediCore.Application.Features.Doctors.Commands.DeleteDoctor;

public sealed record DeleteDoctorCommand(int Id) : IRequest;