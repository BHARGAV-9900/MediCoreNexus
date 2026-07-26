using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryTest;

public class CreateLaboratoryTestCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}