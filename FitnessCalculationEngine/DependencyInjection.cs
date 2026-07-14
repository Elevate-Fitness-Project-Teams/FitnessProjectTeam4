using FitnessCalculationEngine.Common;
using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Messaging;
using FitnessCalculationEngine.Common.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine;

public static class DependencyInjection
{
    public static IServiceCollection AddFce(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<FceDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("FceDb")));

        services.AddScoped<IRepository, Repository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddFceMessaging(config);

        services.AddAuthentication(DevAuthenticationHandler.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, DevAuthenticationHandler>(
                DevAuthenticationHandler.SchemeName, _ => { });
        services.AddAuthorization();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "Fitness Calculation Engine Service",
                Version = "v1",
                Description = "Handles BMR/TDEE calculations, fitness metrics, and plan assignments."
            });
        });

        return services;
    }
}
