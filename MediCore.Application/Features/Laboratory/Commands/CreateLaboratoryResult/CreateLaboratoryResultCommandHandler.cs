using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryResult;

public class CreateLaboratoryResultCommandHandler
    : IRequestHandler<CreateLaboratoryResultCommand, int>
{
    private readonly ILaboratoryResultRepository _resultRepository;
    private readonly ILaboratoryOrderRepository _orderRepository;

    public CreateLaboratoryResultCommandHandler(
        ILaboratoryResultRepository resultRepository,
        ILaboratoryOrderRepository orderRepository)
    {
        _resultRepository = resultRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(
        CreateLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.LaboratoryOrderId,
            cancellationToken);

        if (order is null)
            throw new NotFoundException(
                $"Laboratory order with Id {request.LaboratoryOrderId} was not found.");

        var exists = await _resultRepository.ExistsByLaboratoryOrderAsync(
            request.LaboratoryOrderId,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                "A laboratory result already exists for this laboratory order.");

        var laboratoryResult = new LaboratoryResult(
            request.LaboratoryOrderId,
            request.Result,
            request.Remarks);

        await _resultRepository.AddAsync(
            laboratoryResult,
            cancellationToken);

        await _resultRepository.SaveChangesAsync(
            cancellationToken);

        return laboratoryResult.Id;
    }
}