using ConferenceHalls.Application.ViewModels;

namespace RabbitMQ.Contracts.Responses
{
    public class GetConferenceHallsByIdsResponse
    {
        public ConferenceHallViewModel[] ConferenceHalls { get; set; } = Array.Empty<ConferenceHallViewModel>();
    }
}
