using AutoMapper;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Behaviors;
using ProgressService.Consumers;
using ProgressService.Infrastructure.Data.Contexts;
using ProgressService.Infrastructure.Data.Repositories;
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
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("quest");
                        h.Password("quest");
                    });

                    config.ReceiveEndpoint("session-Started", e =>
                    {
                        e.ConfigureConsumer<SessionStartedConsumer>(context);
                    });
                });
            });

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
