using Users.Application.Services;
using Users.Application.Services.Abstractions;
using Users.Application.Services.Jwt;
using Users.Domain.Abstractions;
using Users.Infrastructure.Persistence.Services.Abstractions;
using Users.Infrastructure.Repositories;

namespace Users.WebApi.DI
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}