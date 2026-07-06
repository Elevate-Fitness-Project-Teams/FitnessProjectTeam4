using Microsoft.EntityFrameworkCore;
using ProfileService.Infrastructure.Data;
using ProfileService.Infrastructure.Repository;
using System;

namespace ProfileService.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection DBConfiguration(this IServiceCollection service, IConfiguration configuration, string Connection)
        {
            service.AddDbContext<ProfileDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Connection)));

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            

            

            return service;
        }
    }
}
