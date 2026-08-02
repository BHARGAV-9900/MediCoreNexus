using MediCore.Application.Features.Patients.Queries.GetPagedPatients;
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
    public async Task<(IEnumerable<Patient> Patients, int TotalCount)> GetPagedAsync(
        GetPagedPatientsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Patient> query = _context.Patients
            .AsNoTracking()
            .Where(p => !p.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Filter.Search))
        {
            var search = request.Filter.Search.Trim().ToLower();

            query = query.Where(p =>
                p.FirstName.ToLower().Contains(search) ||
                p.LastName.ToLower().Contains(search) ||
                p.Email.ToLower().Contains(search));
        }

        // Gender Filter
        if (!string.IsNullOrWhiteSpace(request.Filter.Gender))
        {
            query = query.Where(p =>
                p.Gender.ToString() == request.Filter.Gender);
        }

        // Sorting
        query = (request.Filter.SortBy?.ToLower(),
                 request.Filter.SortDirection?.ToLower()) switch
        {
            ("firstname", "desc") => query.OrderByDescending(x => x.FirstName),

            ("firstname", _) => query.OrderBy(x => x.FirstName),

            ("lastname", "desc") => query.OrderByDescending(x => x.LastName),

            ("lastname", _) => query.OrderBy(x => x.LastName),

            ("createdat", "desc") => query.OrderByDescending(x => x.CreatedAt),

            ("createdat", _) => query.OrderBy(x => x.CreatedAt),

            _ => query.OrderBy(x => x.FirstName)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var patients = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return (patients, totalCount);
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