using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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
        var normalizedEmail = email.Trim().ToLower();

        return await _context.Doctors
            .AnyAsync(
                d => d.Email.ToLower() == normalizedEmail && !d.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsByEmailExceptIdAsync(
        string email,
        int doctorId,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = email.Trim().ToLower();

        return await _context.Doctors
            .AnyAsync(
                d => d.Email.ToLower() == normalizedEmail
                    && d.Id != doctorId
                    && !d.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        var normalizedPhoneNumber = phoneNumber.Trim();

        return await _context.Doctors
            .AnyAsync(
                d => d.PhoneNumber == normalizedPhoneNumber && !d.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsByPhoneNumberExceptIdAsync(
        string phoneNumber,
        int doctorId,
        CancellationToken cancellationToken)
    {
        var normalizedPhoneNumber = phoneNumber.Trim();

        return await _context.Doctors
            .AnyAsync(
                d => d.PhoneNumber == normalizedPhoneNumber
                    && d.Id != doctorId
                    && !d.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
