using FluentValidation;

namespace MediCore.Application.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandValidator
    : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.BatchNumber)
            .NotEmpty();

        RuleFor(x => x.MinimumStockLevel)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Supplier)
            .NotEmpty();

        RuleFor(x => x.StorageLocation)
            .NotEmpty();

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.UtcNow);
    }
}