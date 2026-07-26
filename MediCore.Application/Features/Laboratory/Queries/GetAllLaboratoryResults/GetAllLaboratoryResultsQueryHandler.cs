using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryResults;

public class GetAllLaboratoryResultsQueryHandler
    : IRequestHandler<GetAllLaboratoryResultsQuery, IEnumerable<LaboratoryResultDto>>
{
    private readonly ILaboratoryResultRepository _repository;

    public GetAllLaboratoryResultsQueryHandler(
        ILaboratoryResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LaboratoryResultDto>> Handle(
        GetAllLaboratoryResultsQuery request,
        CancellationToken cancellationToken)
    {
        var results = await _repository.GetAllAsync(cancellationToken);

        return results.Select(r => new LaboratoryResultDto
        {
            Id = r.Id,
            PublicId = r.PublicId,
            LaboratoryOrderId = r.LaboratoryOrderId,
            LaboratoryOrderPublicId = r.LaboratoryOrder!.PublicId,
            Result = r.Result,
            Remarks = r.Remarks
        });
    }
}