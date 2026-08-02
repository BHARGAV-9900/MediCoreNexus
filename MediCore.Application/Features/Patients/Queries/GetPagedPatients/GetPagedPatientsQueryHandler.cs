using AutoMapper;
using MediatR;
using MediCore.Application.Common.Pagination;
using MediCore.Application.Features.Patients.Queries.GetAllPatients;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Patients.Queries.GetPagedPatients;

public class GetPagedPatientsQueryHandler
    : IRequestHandler<GetPagedPatientsQuery, PagedResult<PatientDto>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPagedPatientsQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<PatientDto>> Handle(
        GetPagedPatientsQuery request,
        CancellationToken cancellationToken)
    {
        var (patients, totalCount) =
            await _patientRepository.GetPagedAsync(
                request,
                cancellationToken);

        return new PagedResult<PatientDto>
        {
            Items = _mapper.Map<IEnumerable<PatientDto>>(patients),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}