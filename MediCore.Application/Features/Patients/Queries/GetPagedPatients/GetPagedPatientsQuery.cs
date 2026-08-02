using MediatR;
using MediCore.Application.Common.Filtering;
using MediCore.Application.Common.Pagination;
using MediCore.Application.Features.Patients.Queries.GetAllPatients;

namespace MediCore.Application.Features.Patients.Queries.GetPagedPatients;

public class GetPagedPatientsQuery : IRequest<PagedResult<PatientDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public PatientFilter Filter { get; set; } = new();
}