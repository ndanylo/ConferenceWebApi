using ConferenceHalls.Application.ViewModels;

namespace RabbitMQ.Contracts.Responses
{
    public class GetConferenceServicesByIdsResponse
    {
        public ConferenceServiceViewModel[] ConferenceServices { get; set; } = Array.Empty<ConferenceServiceViewModel>();
    }
}
