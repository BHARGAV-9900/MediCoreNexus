using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Roles.AnyAsync())
        {
            return;
        }

        var roles = new List<Role>
        {
            new("Admin"),
            new("Doctor"),
            new("Receptionist"),
            new("Lab Technician"),
            new("Pharmacist")
        };

        await context.Roles.AddRangeAsync(roles);

        await context.SaveChangesAsync();
    }
}