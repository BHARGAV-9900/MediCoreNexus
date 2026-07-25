using MediatR;
using MediCore.Application.Features.Patients.Queries.GetAllPatients;

namespace MediCore.Application.Features.Patients.Queries.GetPatientById;

public class GetPatientByIdQuery : IRequest<PatientDto>
{
    public int Id { get; set; }
}