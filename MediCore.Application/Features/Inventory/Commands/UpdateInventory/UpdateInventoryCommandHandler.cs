using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandHandler
    : IRequestHandler<UpdateInventoryCommand, bool>
{
    private readonly IInventoryRepository _inventoryRepository;

    public UpdateInventoryCommandHandler(
        IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<bool> Handle(
        UpdateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (inventory is null)
        {
            throw new NotFoundException(
                "Inventory record not found.");
        }

        inventory.Update(
            request.BatchNumber,
            request.MinimumStockLevel,
            request.ExpiryDate,
            request.Supplier,
            request.StorageLocation);

        await _inventoryRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}