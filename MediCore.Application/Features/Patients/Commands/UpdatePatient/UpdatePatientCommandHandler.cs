using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler
    : IRequestHandler<UpdatePatientCommand>
{
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(
        IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task Handle(
        UpdatePatientCommand request,
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

        var duplicatePatient = (await _patientRepository.GetAllAsync(
            cancellationToken))
            .FirstOrDefault(p =>
                p.Email.ToLower() == request.Email.ToLower()
                && p.Id != request.Id);

        if (duplicatePatient is not null)
        {
            throw new ConflictException(
                "A patient with this email already exists.");
        }

        patient.Update(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.BloodGroup,
            request.PhoneNumber,
            request.Email,
            request.Address,
            request.EmergencyContactName,
            request.EmergencyContactPhone,
            request.InsuranceNumber);

        await _patientRepository.SaveChangesAsync(
            cancellationToken);
    }
}