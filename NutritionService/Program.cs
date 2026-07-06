
using Microsoft.EntityFrameworkCore;
using NutritionService.Common;
using NutritionService.Common.Middleware;
using NutritionService.Persistence;
using NutritionService.Persistence.Repositories;

namespace NutritionService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register DbContext
            builder.Services.AddDbContext<NutritionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            
            builder.Services.AddScoped<Common.Services.IFceServiceClient, Common.Services.MockFceServiceClient>();

            var app = builder.Build();

            
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NutritionDbContext>();
                await DataSeeder.SeedAsync(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapAllEndpoints();

            app.Run();
        }
    }
}
