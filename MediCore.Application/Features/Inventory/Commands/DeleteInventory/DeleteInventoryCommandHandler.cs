using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Inventory.Commands.DeleteInventory;

public class DeleteInventoryCommandHandler
    : IRequestHandler<DeleteInventoryCommand, bool>
{
    private readonly IInventoryRepository _inventoryRepository;

    public DeleteInventoryCommandHandler(
        IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<bool> Handle(
        DeleteInventoryCommand request,
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

        inventory.Delete();

        await _inventoryRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}