using FluentValidation;

namespace MediCore.Application.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommandValidator
    : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(x => x.MedicineId)
            .GreaterThan(0);

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MinimumStockLevel)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.BatchNumber)
            .NotEmpty();

        RuleFor(x => x.Supplier)
            .NotEmpty();

        RuleFor(x => x.StorageLocation)
            .NotEmpty();

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.UtcNow);
    }
}