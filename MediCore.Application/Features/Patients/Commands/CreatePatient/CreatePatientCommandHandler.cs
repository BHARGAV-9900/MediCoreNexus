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
        // Normalize values before duplicate checks and persistence.
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedPhoneNumber = request.PhoneNumber.Trim();
        var normalizedEmergencyPhone = request.EmergencyContactPhone.Trim();

        // ---------------------------------------------------------
        // Check duplicate email and phone number
        // ---------------------------------------------------------

        var emailExists = await _patientRepository.ExistsByEmailAsync(
            normalizedEmail,
            cancellationToken);

        var phoneExists = await _patientRepository.ExistsByPhoneNumberAsync(
            normalizedPhoneNumber,
            cancellationToken);

        // Both email and phone already exist
        if (emailExists && phoneExists)
        {
            throw new ConflictException(
                "A patient with this email and phone number already exists.");
        }

        // Only email already exists
        if (emailExists)
        {
            throw new ConflictException(
                "A patient with this email already exists.");
        }

        // Only phone number already exists
        if (phoneExists)
        {
            throw new ConflictException(
                "A patient with this phone number already exists.");
        }

        // ---------------------------------------------------------
        // Create patient
        // ---------------------------------------------------------

        var patient = new Patient(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.BloodGroup,
            normalizedPhoneNumber,
            normalizedEmail,
            request.Address,
            request.EmergencyContactName,
            normalizedEmergencyPhone,
            request.InsuranceNumber);

        await _patientRepository.AddAsync(
            patient,
            cancellationToken);

        await _patientRepository.SaveChangesAsync(
            cancellationToken);

        return patient.Id;
    }
}
