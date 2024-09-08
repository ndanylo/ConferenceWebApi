using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.EF;
using Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Users.WebApi.DI
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
                
            return services;
        }
    }
}