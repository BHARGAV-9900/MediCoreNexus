using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediCore.Application.Interfaces.Services;
using MediCore.Infrastructure.Authentication;
using MediCore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MediCore.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Services here
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        var jwtSettings = configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("==================================");
                        Console.WriteLine("JWT AUTHENTICATION FAILED");
                        Console.WriteLine(context.Exception);
                        Console.WriteLine("==================================");

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            // ==========================================
            // ADMIN
            // ==========================================

            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));


            // ==========================================
            // PATIENTS
            // ==========================================

            options.AddPolicy("PatientManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));


            // ==========================================
            // APPOINTMENTS
            // ==========================================

            options.AddPolicy("AppointmentManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));


            // ==========================================
            // DOCTORS
            // ==========================================

            options.AddPolicy("DoctorView", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));

            options.AddPolicy("DoctorManagement", policy =>
                policy.RequireRole("Admin"));


            // ==========================================
            // DEPARTMENTS
            // ==========================================

            options.AddPolicy("DepartmentView", policy =>
                policy.RequireRole(
                    "Admin",
                    "Receptionist"));

            options.AddPolicy("DepartmentManagement", policy =>
                policy.RequireRole("Admin"));


            // ==========================================
            // MEDICAL RECORDS
            // ==========================================

            options.AddPolicy("MedicalRecordManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor"));


            // ==========================================
            // LABORATORY
            // ==========================================

            options.AddPolicy("LaboratoryManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Lab Technician"));


            // ==========================================
            // PHARMACY
            // ==========================================

            options.AddPolicy("PharmacyManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Pharmacist"));


            // ==========================================
            // PRESCRIPTIONS
            // ==========================================

            options.AddPolicy("PrescriptionManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Pharmacist"));


            // ==========================================
            // BILLING
            // ==========================================

            options.AddPolicy("BillingManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Accountant",
                    "Receptionist"));


            // ==========================================
            // PAYMENTS
            // ==========================================

            options.AddPolicy("PaymentManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Accountant",
                    "Receptionist"));


            // ==========================================
            // NOTIFICATIONS
            // ==========================================

            options.AddPolicy("NotificationManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist",
                    "Lab Technician",
                    "Pharmacist",
                    "Accountant"));


            // ==========================================
            // INVENTORY
            // ==========================================

            options.AddPolicy("InventoryManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Pharmacist"));


            //=============================================
            // REPORTS
            //============================================

            options.AddPolicy("ReportsManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Accountant"));
                    });

        return services;
    }
}