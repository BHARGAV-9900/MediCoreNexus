using AutoMapper;
using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Queries.GetExpiringInventory;

public class GetExpiringInventoryQueryHandler
    : IRequestHandler<GetExpiringInventoryQuery, IEnumerable<InventoryDto>>
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public GetExpiringInventoryQueryHandler(
        IInventoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryDto>> Handle(
        GetExpiringInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetExpiringAsync(
            request.Days,
            cancellationToken);

        return _mapper.Map<IEnumerable<InventoryDto>>(inventory);
    }
}