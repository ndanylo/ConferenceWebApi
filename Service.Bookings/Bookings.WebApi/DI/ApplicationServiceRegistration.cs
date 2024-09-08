using Bookings.Application.Services;
using Bookings.Application.Services.Abstractions;
using Bookings.Application.Messaging.Services;
using Bookings.Application.Messaging.Services.ConferenceHall;

namespace Bookings.WebApi.DI
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IConferenceHallService, ConferenceHallService>();
            services.AddScoped<IPriceAdjustmentService, PriceAdjustmentService>();
            return services;
        }
    }
}
