using Elevate.Fce.Common;
using Elevate.Fce.Common.Messaging;
using Elevate.Fce.Common.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Fce;

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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
