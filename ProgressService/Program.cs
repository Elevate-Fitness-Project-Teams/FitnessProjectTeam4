using AutoMapper;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Behaviors;
using ProgressService.Consumers;
using ProgressService.Infrastructure.Data.Contexts;
using ProgressService.Infrastructure.Data.Repositories;
using Serilog;
using SharedMessages.Queues;
using System.Reflection;

namespace ProgressService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register Infrastructure Services
            builder.Services.AddDbContext<ProgressDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add MediatR  
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Add FluentValidation
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register RabbitMQ
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SessionStartedConsumer>();

                x.AddEntityFrameworkOutbox<ProgressDbContext>(opt =>
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

                    config.ReceiveEndpoint(QueueNames.SessionStarted, e =>
                    {
                        e.ConfigureConsumer<SessionStartedConsumer>(context);
                        e.UseEntityFrameworkOutbox<ProgressDbContext>(context);
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

            // Configure the HTTP request pipeline.

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
