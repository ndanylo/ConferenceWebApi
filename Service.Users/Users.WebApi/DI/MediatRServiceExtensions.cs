
using Users.Application.Commands.RegisterUser;
using Users.Application.Commands.LoginUser;

namespace Users.WebApi.DI
{
    public static class MediatRServiceExtensions
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginUserCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            });

            return services;
        }
    }
}
