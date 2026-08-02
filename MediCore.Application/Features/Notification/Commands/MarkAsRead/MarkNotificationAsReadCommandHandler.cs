using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Commands.MarkAsRead;

public class MarkNotificationAsReadCommandHandler
    : IRequestHandler<MarkNotificationAsReadCommand, bool>
{
    private readonly INotificationRepository _repository;

    public MarkNotificationAsReadCommandHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        MarkNotificationAsReadCommand request,
        CancellationToken cancellationToken)
    {
        var notification =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (notification is null)
            throw new NotFoundException("Notification not found.");

        notification.MarkAsRead();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}