
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Common.Middlewares;
using ProfileService.Common.PipelineBehaviors;
using ProfileService.Domain.Common;
using ProfileService.Infrastructure;
using ProfileService.Infrastructure.Data;
using ProfileService.Infrastructure.DI;

namespace ProfileService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.DBConfiguration(builder.Configuration, "DefaultConnection");

            // AutoMapper

            builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

            // MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            


            builder.Services.AddScoped<GlobalErrorHandling>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalErrorHandling();


            app.MapControllers();

            app.Run();
        }
    }
}
