using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryOrderById;

public class GetLaboratoryOrderByIdQueryHandler
    : IRequestHandler<GetLaboratoryOrderByIdQuery, LaboratoryOrderDto>
{
    private readonly ILaboratoryOrderRepository _repository;

    public GetLaboratoryOrderByIdQueryHandler(
        ILaboratoryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<LaboratoryOrderDto> Handle(
        GetLaboratoryOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (order is null)
            throw new NotFoundException(
                $"Laboratory order with Id {request.Id} was not found.");

        return new LaboratoryOrderDto
        {
            Id = order.Id,
            PublicId = order.PublicId,
            AppointmentId = order.AppointmentId,
            AppointmentPublicId = order.Appointment!.PublicId,
            LaboratoryTestId = order.LaboratoryTestId,
            LaboratoryTestPublicId = order.LaboratoryTest!.PublicId
        };
    }
}