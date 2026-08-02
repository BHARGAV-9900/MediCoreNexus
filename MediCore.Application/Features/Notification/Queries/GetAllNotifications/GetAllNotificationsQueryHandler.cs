using AutoMapper;
using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetAllNotifications;

public class GetAllNotificationsQueryHandler
    : IRequestHandler<GetAllNotificationsQuery,
        IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;
    private readonly IMapper _mapper;

    public GetAllNotificationsQueryHandler(
        INotificationRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(
        GetAllNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications =
            await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<NotificationDto>>(
            notifications);
    }
}