using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class SystemSettingsRepository
    : ISystemSettingsRepository
{
    private readonly ApplicationDbContext _context;

    public SystemSettingsRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SystemSettings?> GetAsync(
        CancellationToken cancellationToken)
    {
        return await _context.SystemSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                cancellationToken);
    }

    public async Task<SystemSettings?> GetForUpdateAsync(
        CancellationToken cancellationToken)
    {
        return await _context.SystemSettings
            .FirstOrDefaultAsync(
                cancellationToken);
    }

    public async Task AddAsync(
        SystemSettings settings,
        CancellationToken cancellationToken)
    {
        await _context.SystemSettings.AddAsync(
            settings,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}