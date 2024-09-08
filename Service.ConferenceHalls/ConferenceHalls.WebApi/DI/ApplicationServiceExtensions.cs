using ConferenceHalls.Application.Messaging.Services.Booking;
using ConferenceHalls.Domain.Abstraction;
using ConferenceHalls.Infrastructure.Repositories;
using ConferenseHalls.Application.Messaging.Services;

namespace ConferenceHalls.WebApi.DI
{
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConferenceHallRepository, ConferenceHallRepository>();
            services.AddScoped<IConferenceServiceRepository, ConferenceServiceRepository>();
            services.AddScoped<IBookingService, BookingService>();
            return services;
        }
    }
}
