using MediatR;
using MediCore.Application.Interfaces.Repositories;
using InventoryEntity = MediCore.Domain.Entities.Inventory;

namespace MediCore.Application.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommandHandler
    : IRequestHandler<CreateInventoryCommand, int>
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryCommandHandler(
        IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<int> Handle(
        CreateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var inventory = new InventoryEntity(
            request.MedicineId,
            request.BatchNumber,
            request.QuantityInStock,
            request.MinimumStockLevel,
            request.ExpiryDate,
            request.Supplier,
            request.StorageLocation);

        await _inventoryRepository.AddAsync(
            inventory,
            cancellationToken);

        await _inventoryRepository.SaveChangesAsync(
            cancellationToken);

        return inventory.Id;
    }
}