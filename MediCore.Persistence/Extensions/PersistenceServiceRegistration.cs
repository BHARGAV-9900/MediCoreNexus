using MediCore.Application.Interfaces.Repositories;
using MediCore.Persistence.Context;
using MediCore.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediCore.Persistence.Extensions;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)

    
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();

        return services;
    }
    
}