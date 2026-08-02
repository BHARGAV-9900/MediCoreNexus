using AutoMapper;
using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetUnreadNotifications;

public class GetUnreadNotificationsQueryHandler
    : IRequestHandler<
        GetUnreadNotificationsQuery,
        IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;
    private readonly IMapper _mapper;

    public GetUnreadNotificationsQueryHandler(
        INotificationRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(
        GetUnreadNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications =
            await _repository.GetUnreadAsync(cancellationToken);

        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }
}