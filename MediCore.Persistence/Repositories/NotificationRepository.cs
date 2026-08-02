using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class NotificationRepository
    : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(
            notification,
            cancellationToken);
    }

    public async Task<Notification?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Notification>> GetUnreadAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(x =>
                !x.IsDeleted &&
                !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}