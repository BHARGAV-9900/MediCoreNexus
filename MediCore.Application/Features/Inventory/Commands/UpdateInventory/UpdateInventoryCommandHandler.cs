using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandHandler
    : IRequestHandler<UpdateInventoryCommand, bool>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly INotificationService _notificationService;

    public UpdateInventoryCommandHandler(
        IInventoryRepository inventoryRepository,
        INotificationService notificationService)
    {
        _inventoryRepository = inventoryRepository;
        _notificationService = notificationService;
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

        var wasLowStock = inventory.IsLowStock;

        inventory.UpdateStock(request.QuantityInStock);

        inventory.Update(
            request.BatchNumber,
            request.MinimumStockLevel,
            request.ExpiryDate,
            request.Supplier,
            request.StorageLocation);

        await _inventoryRepository.SaveChangesAsync(
            cancellationToken);

        if (!wasLowStock && inventory.IsLowStock)
        {
            var medicineName = inventory.Medicine?.Name
                ?? $"Medicine #{inventory.MedicineId}";

            await _notificationService.NotifyRolesAsync(
                new[]
                {
                    UserRole.Administrator,
                    UserRole.Pharmacist
                },
                "Low Stock Alert",
                $"Low stock alert: {medicineName} (Batch {inventory.BatchNumber}) has {inventory.QuantityInStock} units remaining, which is at or below the minimum stock level of {inventory.MinimumStockLevel}.",
                "Inventory",
                cancellationToken);
        }

        return true;
    }
}