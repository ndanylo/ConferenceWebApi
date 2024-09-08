using ConferenceHalls.Application.Mapping;
using System.Reflection;

namespace ConferenceHalls.WebApi.DI
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
