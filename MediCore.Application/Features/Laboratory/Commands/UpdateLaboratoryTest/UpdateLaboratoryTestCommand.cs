using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryTest;

public class UpdateLaboratoryTestCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}