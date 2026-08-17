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

        var patients = await _patientRepository.GetAllAsync(
            cancellationToken);

        var normalizedEmail = request.Email.Trim().ToLower();
        var normalizedPhoneNumber = request.PhoneNumber.Trim();

        var duplicateEmail = patients.FirstOrDefault(p =>
            p.Email.ToLower() == normalizedEmail
            && p.Id != request.Id);

        if (duplicateEmail is not null)
        {
            throw new ConflictException(
                "A patient with this email already exists.");
        }

        var duplicatePhoneNumber = patients.FirstOrDefault(p =>
            p.PhoneNumber == normalizedPhoneNumber
            && p.Id != request.Id);

        if (duplicatePhoneNumber is not null)
        {
            throw new ConflictException(
                "A patient with this phone number already exists.");
        }

        patient.Update(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.BloodGroup,
            normalizedPhoneNumber,
            normalizedEmail,
            request.Address,
            request.EmergencyContactName,
            request.EmergencyContactPhone,
            request.InsuranceNumber);

        await _patientRepository.SaveChangesAsync(
            cancellationToken);
    }
}