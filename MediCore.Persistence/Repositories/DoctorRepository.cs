using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace MediCore.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
            Doctor doctor,
            CancellationToken cancellationToken)
    {
        await _context.Doctors.AddAsync(doctor, cancellationToken);
    }

    public async Task<Doctor?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
    {
        return await _context.Doctors
            .Include(d => d.Department)
            .FirstOrDefaultAsync(
                d => d.Id == id && !d.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        return await _context.Doctors
            .Include(d => d.Department)
            .Where(d => !d.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken)
    {
        return await _context.Doctors
            .AnyAsync(
                d => d.Email == email && !d.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}