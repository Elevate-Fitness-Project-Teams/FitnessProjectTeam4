using FitnessCalculationEngine.Features.Recalculation.Consumers;
using MassTransit;

namespace FitnessCalculationEngine.Common.Messaging;

public static class MassTransitExtensions
{
    public static IServiceCollection AddFceMessaging(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<WeightUpdatedConsumer>();

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                var host = config["RabbitMq:Host"] ?? "localhost";
                var vhost = config["RabbitMq:VHost"] ?? "/";
                var user = config["RabbitMq:User"] ?? "guest";
                var pass = config["RabbitMq:Pass"] ?? "guest";

                cfg.Host(host, vhost, h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
