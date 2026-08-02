using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Commands.DeleteNotification;

public class DeleteNotificationCommandHandler
    : IRequestHandler<DeleteNotificationCommand, bool>
{
    private readonly INotificationRepository _repository;

    public DeleteNotificationCommandHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var notification =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (notification is null)
            throw new NotFoundException("Notification not found.");

        notification.Delete();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}