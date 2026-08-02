using AutoMapper;
using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler
    : IRequestHandler<GetNotificationByIdQuery, NotificationDto>
{
    private readonly INotificationRepository _repository;
    private readonly IMapper _mapper;

    public GetNotificationByIdQueryHandler(
        INotificationRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<NotificationDto> Handle(
        GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var notification = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (notification is null)
            throw new NotFoundException("Notification not found.");

        return _mapper.Map<NotificationDto>(notification);
    }
}