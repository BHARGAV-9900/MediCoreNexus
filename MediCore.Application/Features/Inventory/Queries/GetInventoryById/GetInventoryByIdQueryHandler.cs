using AutoMapper;
using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Queries.GetInventoryById;

public class GetInventoryByIdQueryHandler
    : IRequestHandler<GetInventoryByIdQuery, InventoryDto>
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public GetInventoryByIdQueryHandler(
        IInventoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InventoryDto> Handle(
        GetInventoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (inventory is null)
        {
            throw new NotFoundException(
                "Inventory not found.");
        }

        return _mapper.Map<InventoryDto>(inventory);
    }
}