using MediCore.Persistence.Context;
using MediCore.Persistence.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MediCore.IntegrationTests.Infrastructure;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private const string TestConnectionString =
        "Server=.\\MSSQLSERVER02;Database=MediCoreNexusTestDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.UseSetting(
            "ConnectionStrings:DefaultConnection",
            TestConnectionString);

        builder.ConfigureServices(services =>
        {
            using var serviceProvider =
                services.BuildServiceProvider();

            using var scope =
                serviceProvider.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync()
                .GetAwaiter()
                .GetResult();

            RoleSeeder.SeedAsync(context)
                .GetAwaiter()
                .GetResult();
        });
    }
}