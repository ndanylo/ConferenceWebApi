using Microsoft.EntityFrameworkCore;
using ConferenceHalls.Infrastructure.Persistence;

namespace ConferenceHalls.WebApi.DI
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConferenceDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
