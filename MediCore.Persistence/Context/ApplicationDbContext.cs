using System.Text.Json;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Common;
using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    private static readonly HashSet<string> AuditExcludedProperties =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "CreatedAt",
            "CreatedBy",
            "UpdatedAt",
            "UpdatedBy",
            "PasswordHash",
            "RefreshToken",
            "RefreshTokenHash",
            "Token",
            "TokenHash",
            "SecretKey"
        };

    private readonly ICurrentUserService? _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService? currentUserService = null)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var now = DateTime.UtcNow;
        var currentUserEmail = _currentUserService?.Email;

        var auditableEntries = ChangeTracker
            .Entries<BaseAuditableEntity>()
            .Where(entry => entry.State is
                EntityState.Added or
                EntityState.Modified or
                EntityState.Deleted)
            .ToList();

        foreach (var entry in auditableEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = currentUserEmail;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = currentUserEmail;
            }
        }

        var auditLogs = auditableEntries
            .Select(CreateAuditLog)
            .Where(log => log is not null)
            .Cast<AuditLog>()
            .ToList();

        if (auditLogs.Count > 0)
        {
            await AuditLogs.AddRangeAsync(
                auditLogs,
                cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<LaboratoryTest> LaboratoryTests => Set<LaboratoryTest>();
    public DbSet<LaboratoryOrder> LaboratoryOrders => Set<LaboratoryOrder>();
    public DbSet<LaboratoryResult> LaboratoryResults => Set<LaboratoryResult>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<BillItem> BillItems => Set<BillItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<SystemSettings> SystemSettings => Set<SystemSettings>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private AuditLog? CreateAuditLog(
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseAuditableEntity> entry)
    {
        var entity = entry.Entity;
        var entityType = entry.Metadata.ClrType.Name;
        var action = GetAuditAction(entry);

        if (string.IsNullOrWhiteSpace(action))
            return null;

        var oldValues = new Dictionary<string, object?>();
        var newValues = new Dictionary<string, object?>();

        foreach (var property in entry.Properties)
        {
            if (AuditExcludedProperties.Contains(property.Metadata.Name))
                continue;

            if (entry.State == EntityState.Added)
            {
                newValues[property.Metadata.Name] = property.CurrentValue;
                continue;
            }

            if (entry.State == EntityState.Deleted)
            {
                oldValues[property.Metadata.Name] = property.OriginalValue;
                continue;
            }

            if (!property.IsModified)
                continue;

            oldValues[property.Metadata.Name] = property.OriginalValue;
            newValues[property.Metadata.Name] = property.CurrentValue;
        }

        var publicId = entity.PublicId == Guid.Empty
            ? null
            : entity.PublicId.ToString();

        return new AuditLog(
            _currentUserService?.UserId,
            _currentUserService?.Email,
            _currentUserService?.Role,
            action,
            entityType,
            publicId,
            SerializeValues(oldValues),
            SerializeValues(newValues),
            _currentUserService?.IpAddress,
            _currentUserService?.RequestPath,
            _currentUserService?.RequestId,
            DateTime.UtcNow);
    }

    private static string GetAuditAction(
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseAuditableEntity> entry)
    {
        if (entry.State == EntityState.Added)
            return "Created";

        if (entry.State == EntityState.Deleted)
            return "Deleted";

        if (entry.State == EntityState.Modified &&
            entry.Entity.IsDeleted &&
            entry.OriginalValues.GetValue<bool>(nameof(BaseAuditableEntity.IsDeleted)) == false)
        {
            return "Deleted";
        }

        if (entry.State == EntityState.Modified)
            return "Updated";

        return string.Empty;
    }

    private static string? SerializeValues(
        IReadOnlyDictionary<string, object?> values)
    {
        return values.Count == 0
            ? null
            : JsonSerializer.Serialize(values);
    }
}
