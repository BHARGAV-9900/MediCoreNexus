using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Patients.Queries.GetAllPatients;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryHandler
    : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdQueryHandler(
        IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto> Handle(
        GetPatientByIdQuery request,
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

        return new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender.ToString(),
            BloodGroup = patient.BloodGroup.ToString(),
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            Address = patient.Address,
            IsActive = patient.IsActive
        };
    }
}