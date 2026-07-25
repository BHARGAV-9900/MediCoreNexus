using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler
    : IRequestHandler<DeletePatientCommand>
{
    private readonly IPatientRepository _patientRepository;

    public DeletePatientCommandHandler(
        IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task Handle(
        DeletePatientCommand request,
        CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (patient is null)
        {
            throw new NotFoundException(
                $"Patient with Id {request.Id} was not found.");
        }

        patient.Delete();

        await _patientRepository.SaveChangesAsync(
            cancellationToken);
    }
}