using System.Reflection;
using ConferenceHalls.Application.Commands;
using ConferenceHalls.Application.ConferenceHalls.Commands;
using ConferenceHalls.Application.Queries;

namespace ConferenceHalls.WebApi.DI
{
    public static class MediatRServiceExtensions
    {
        public static IServiceCollection AddMediatRConfigurationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(CreateConferenceHallCommand).Assembly,
                    typeof(CreateServiceCommand).Assembly,
                    typeof(DeleteConferenceHallCommand).Assembly,
                    typeof(DeleteServiceCommand).Assembly,
                    typeof(DeleteServiceCommand).Assembly,
                    typeof(RestoreConferenceHallCommand).Assembly,
                    typeof(AddServicesToConferenceHallCommand).Assembly,
                    typeof(UpdateRentPriceCommand).Assembly,
                    typeof(GetAllConferenceHallsQuery).Assembly,
                    typeof(GetAllServicesQuery).Assembly,
                    typeof(GetConferenceHallsByCapacityQuery).Assembly,
                    typeof(GetConferenceHallsByIdsQuery).Assembly,
                    typeof(GetServicesByConferenceHallIdQuery).Assembly,
                    typeof(GetServicesByIdsQuery).Assembly,
                    Assembly.GetExecutingAssembly()
                );
            });

            return services;
        }
    }
}
