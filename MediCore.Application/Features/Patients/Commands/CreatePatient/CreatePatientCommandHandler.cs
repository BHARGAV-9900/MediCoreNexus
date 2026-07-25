using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler
    : IRequestHandler<CreatePatientCommand, int>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(
        IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<int> Handle(
        CreatePatientCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _patientRepository.ExistsByEmailAsync(
            request.Email,
            cancellationToken);

        if (exists)
        {
            throw new ConflictException(
                "A patient with this email already exists.");
        }

        var patient = new Patient(
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

        await _patientRepository.AddAsync(
            patient,
            cancellationToken);

        await _patientRepository.SaveChangesAsync(
            cancellationToken);

        return patient.Id;
    }
}