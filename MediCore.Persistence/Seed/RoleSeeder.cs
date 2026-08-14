using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var requiredRoles = new[]
        {
            "Admin",
            "Doctor",
            "Receptionist",
            "Lab Technician",
            "Pharmacist",
            "Accountant"
        };

        foreach (var roleName in requiredRoles)
        {
            var exists = await context.Roles
                .AnyAsync(r => r.Name == roleName);

            if (!exists)
            {
                await context.Roles.AddAsync(
                    new Role(roleName));
            }
        }

        await context.SaveChangesAsync();
    }
}