
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProfileService.Common.Middlewares;
using ProfileService.Common.PipelineBehaviors;
using ProfileService.Common.Services;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Domain.Common;
using ProfileService.Infrastructure;
using ProfileService.Infrastructure.Data;
using ProfileService.Infrastructure.DI;
using ProfileService.Infrastructure.Repository;

namespace ProfileService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.DBConfiguration(builder.Configuration, "DefaultConnection");

            builder.Services.AddScoped<GlobalErrorHandling>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<ICurrentUserService, MockCurrentUserService>();
            builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

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




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Enter your JWT token in the text input below.",
                    In = ParameterLocation.Header
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalErrorHandling();


            app.MapControllers();

            app.Run();
        }
    }
}
