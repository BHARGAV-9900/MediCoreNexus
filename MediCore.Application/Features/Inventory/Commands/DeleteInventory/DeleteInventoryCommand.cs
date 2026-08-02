using MediatR;

namespace MediCore.Application.Features.Inventory.Commands.DeleteInventory;

public record DeleteInventoryCommand(int Id) : IRequest<bool>;