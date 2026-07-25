using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler
    : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task Handle(
        DeleteDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(
                $"Doctor with Id {request.Id} was not found.");
        }

        doctor.Delete();

        await _doctorRepository.SaveChangesAsync(cancellationToken);
    }
}