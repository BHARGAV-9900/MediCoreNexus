using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryResultById;

public class GetLaboratoryResultByIdQueryHandler
    : IRequestHandler<GetLaboratoryResultByIdQuery, LaboratoryResultDto>
{
    private readonly ILaboratoryResultRepository _repository;

    public GetLaboratoryResultByIdQueryHandler(
        ILaboratoryResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<LaboratoryResultDto> Handle(
        GetLaboratoryResultByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (result is null)
            throw new NotFoundException(
                $"Laboratory result with Id {request.Id} was not found.");

        return new LaboratoryResultDto
        {
            Id = result.Id,
            PublicId = result.PublicId,
            LaboratoryOrderId = result.LaboratoryOrderId,
            LaboratoryOrderPublicId = result.LaboratoryOrder!.PublicId,
            Result = result.Result,
            Remarks = result.Remarks
        };
    }
}