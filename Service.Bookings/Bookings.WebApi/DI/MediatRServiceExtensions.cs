using System.Reflection;
using Bookings.Application.Commands;
using Bookings.Application.Queries;

namespace Bookings.WebApi.DI
{
    public static class MediatRServiceExtensions
    {
        public static IServiceCollection AddMediatRConfigurationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                 cfg.RegisterServicesFromAssemblies(
                    typeof(CreateBookingCommand).Assembly,
                    typeof(DeleteBookingCommand).Assembly,
                    typeof(DeleteBookingsByConferenceHallIdCommand).Assembly,
                    typeof(RemoveServiceFromBookingsCommand).Assembly,
                    typeof(CheckAvailabilityQuery).Assembly,
                    typeof(GetBookingsByConferenceHallIdQuery).Assembly,
                    typeof(GetBookingsByDateQuery).Assembly,
                    typeof(GetBookingsByUserIdQuery).Assembly,
                    typeof(SearchAvailableHallsQuery).Assembly,
                    Assembly.GetExecutingAssembly()
                );
            });

            return services;
        }
    }
}
