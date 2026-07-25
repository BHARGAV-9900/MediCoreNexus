using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Patient patient,
        CancellationToken cancellationToken)
    {
        await _context.Patients.AddAsync(
            patient,
            cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(
                p => p.Id == id && !p.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AnyAsync(
                p => p.Email == email && !p.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}