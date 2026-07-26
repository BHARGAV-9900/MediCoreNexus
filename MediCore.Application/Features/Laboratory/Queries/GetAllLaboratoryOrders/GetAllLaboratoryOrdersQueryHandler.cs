using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryOrders;

public class GetAllLaboratoryOrdersQueryHandler
    : IRequestHandler<GetAllLaboratoryOrdersQuery, IEnumerable<LaboratoryOrderDto>>
{
    private readonly ILaboratoryOrderRepository _repository;

    public GetAllLaboratoryOrdersQueryHandler(
        ILaboratoryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LaboratoryOrderDto>> Handle(
        GetAllLaboratoryOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync(cancellationToken);

        return orders.Select(o => new LaboratoryOrderDto
        {
            Id = o.Id,
            PublicId = o.PublicId,
            AppointmentId = o.AppointmentId,
            AppointmentPublicId = o.Appointment!.PublicId,
            LaboratoryTestId = o.LaboratoryTestId,
            LaboratoryTestPublicId = o.LaboratoryTest!.PublicId
        });
    }
}