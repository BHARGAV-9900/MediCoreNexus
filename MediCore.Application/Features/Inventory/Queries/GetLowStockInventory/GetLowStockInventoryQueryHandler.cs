using AutoMapper;
using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Queries.GetLowStockInventory;

public class GetLowStockInventoryQueryHandler
    : IRequestHandler<GetLowStockInventoryQuery, IEnumerable<InventoryDto>>
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public GetLowStockInventoryQueryHandler(
        IInventoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryDto>> Handle(
        GetLowStockInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetLowStockAsync(
            cancellationToken);

        return _mapper.Map<IEnumerable<InventoryDto>>(inventory);
    }
}