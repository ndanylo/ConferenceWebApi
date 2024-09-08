using MassTransit;
using ConferenceHalls.Appllication.Messaging.Consumers;
using ConferenceHallService.Application.Messaging.Consumers;
using RabbitMQ.Contracts.Requests;

namespace ConferenceHalls.WebApi.DI
{
    public static class MassTransitServiceExtensions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddRequestClient<DeleteBookingsByConferenceHallIdRequest>();
                x.AddRequestClient<RemoveServiceFromBookingsRequest>();

                x.AddConsumer<GetConferenceHallsByIdsConsumer>();
                x.AddConsumer<GetConferenceServicesByIdsConsumer>();
                x.AddConsumer<GetConferenceHallsByCapacityConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMQSection = configuration.GetSection("RabbitMQ");

                    cfg.Host(rabbitMQSection["Host"] ?? throw new ArgumentNullException("RabbitMQ host is not configured."), "/", h =>
                    {
                        h.Username(rabbitMQSection["Username"] ?? throw new ArgumentNullException("RabbitMQ Username is not configured."));
                        h.Password(rabbitMQSection["Password"] ?? throw new ArgumentNullException("RabbitMQ Password is not configured."));
                    });

                    cfg.ReceiveEndpoint("get-conference-halls-by-ids", e =>
                    {
                        e.ConfigureConsumer<GetConferenceHallsByIdsConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("get-conference-services-by-ids", e =>
                    {
                        e.ConfigureConsumer<GetConferenceServicesByIdsConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("get-conference-halls-by-capacity", e =>
                    {
                        e.ConfigureConsumer<GetConferenceHallsByCapacityConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
