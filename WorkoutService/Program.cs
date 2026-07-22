using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedMessages.Queues;
using System.Reflection;
using WorkoutService.Common.Behaviors;
using WorkoutService.Consumers;
using WorkoutService.Infrastructure.Data.Contexts;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Register Infrastructure Services
            builder.Services.AddDbContext<WorkoutDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Register AutoMapper
            builder.Services.AddAutoMapper(cfg => { },typeof(Program).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register RabbitMQ
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SessionCompletedConsumer>();

                x.AddEntityFrameworkOutbox<WorkoutDbContext>(opt =>
                {
                    opt.UseSqlServer();
                    opt.UseBusOutbox();
                    opt.QueryDelay = TimeSpan.FromSeconds(1); // Delay Before Querying Outbox Messages 
                });

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("quest");
                        h.Password("quest");
                    });

                    config.ReceiveEndpoint(QueueNames.WorkoutProgressLogged, e =>
                    {
                        e.ConfigureConsumer<SessionCompletedConsumer>(context);
                        e.UseEntityFrameworkOutbox<WorkoutDbContext>(context);
                    });
                });
            });

            // Register Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .Enrich.WithEnvironmentName().Enrich.WithMachineName()
                .CreateLogger();

            // Use Serilog for Logging
            builder.Host.UseSerilog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
