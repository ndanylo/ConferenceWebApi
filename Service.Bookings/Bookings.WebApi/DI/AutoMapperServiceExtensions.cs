using Bookings.Application.Mapping;
using System.Reflection;

namespace Bookings.WebApi.DI
{
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
