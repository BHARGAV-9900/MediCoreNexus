using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Patients.Queries.GetAllPatients;

public class GetAllPatientsQueryHandler
    : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsQueryHandler(
        IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientDto>> Handle(
        GetAllPatientsQuery request,
        CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(
            cancellationToken);

        return patients.Select(patient => new PatientDto
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
        });
    }
}