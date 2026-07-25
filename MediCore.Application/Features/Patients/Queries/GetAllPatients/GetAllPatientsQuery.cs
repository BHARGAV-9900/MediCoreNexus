using MediatR;

namespace MediCore.Application.Features.Patients.Queries.GetAllPatients;

public class GetAllPatientsQuery
    : IRequest<IEnumerable<PatientDto>>
{
}