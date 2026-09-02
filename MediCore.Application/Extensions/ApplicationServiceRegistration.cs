using AutoMapper;
using FluentValidation;
using MediCore.Application.Behaviors;
using MediatR;
using MediCore.Application.Features.Departments.Commands.CreateDepartment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MediCore.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register AutoMapper
        services.AddSingleton<IMapper>(serviceProvider =>
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddMaps(Assembly.GetExecutingAssembly());
                },
                loggerFactory);

            return configuration.CreateMapper();
        });

        // Register FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();

        // Register pipeline behaviors
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}