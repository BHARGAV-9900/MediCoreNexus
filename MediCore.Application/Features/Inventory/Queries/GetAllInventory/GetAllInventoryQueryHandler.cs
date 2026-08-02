using AutoMapper;
using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Queries.GetAllInventory;

public class GetAllInventoryQueryHandler
    : IRequestHandler<GetAllInventoryQuery, IEnumerable<InventoryDto>>
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllInventoryQueryHandler(
        IInventoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryDto>> Handle(
        GetAllInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetAllAsync(
            cancellationToken);

        return _mapper.Map<IEnumerable<InventoryDto>>(inventory);
    }
}