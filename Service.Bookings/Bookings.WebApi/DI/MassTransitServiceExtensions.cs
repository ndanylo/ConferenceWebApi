using MassTransit;
using Bookings.Application.Consumers;
using Bookings.Application.Messaging.Consumers;
using RabbitMQ.Contracts.Requests;

namespace Bookings.WebApi.DI
{
    public static class MassTransitServiceExtensions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddRequestClient<GetConferenceHallsByCapacityRequest>();
                x.AddRequestClient<GetConferenceHallsByIdsRequest>();
                x.AddRequestClient<GetConferenceServicesByIdsRequest>();

                x.AddConsumer<DeleteBookingsByConferenceHallConsumer>();
                x.AddConsumer<RemoveServiceFromBookingsConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMQSection = configuration.GetSection("RabbitMQ");

                    cfg.Host(rabbitMQSection["Host"] ?? throw new ArgumentNullException("RabbitMQ host is not configured."), "/", h =>
                    {
                        h.Username(rabbitMQSection["Username"] ?? throw new ArgumentNullException("RabbitMQ Username is not configured."));
                        h.Password(rabbitMQSection["Password"] ?? throw new ArgumentNullException("RabbitMQ Password is not configured."));
                    });

                    cfg.ReceiveEndpoint("delete-bookings-by-conference-hall", e =>
                    {
                        e.ConfigureConsumer<DeleteBookingsByConferenceHallConsumer>(context);
                    });
                    
                    cfg.ReceiveEndpoint("remove-service-from-bookings", e =>
                    {
                        e.ConfigureConsumer<RemoveServiceFromBookingsConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
