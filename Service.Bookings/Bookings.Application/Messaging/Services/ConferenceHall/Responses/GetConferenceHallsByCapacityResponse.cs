using Bookings.Application.ViewModels;

namespace RabbitMQ.Contracts.Responses
{
    public class GetConferenceHallsByCapacityResponse
    {
        public ConferenceHallViewModel[] ConferenceHalls { get; set; } = Array.Empty<ConferenceHallViewModel>();
    }
}
