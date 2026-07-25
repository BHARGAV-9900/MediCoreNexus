using FluentValidation;
using MediCore.Application.Behaviors;
using MediatR;
using MediCore.Application.Features.Departments.Commands.CreateDepartment;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();

        // Register pipeline behaviors
        services.AddTransient(
        typeof(IPipelineBehavior<,>),
        typeof(ValidationBehavior<,>));

        return services;
    }
}