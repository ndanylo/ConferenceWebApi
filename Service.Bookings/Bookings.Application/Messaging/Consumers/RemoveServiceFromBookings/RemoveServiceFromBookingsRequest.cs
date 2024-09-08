using Bookings.Application.ViewModels;

namespace RabbitMQ.Contracts.Requests
{
    public class RemoveServiceFromBookingsRequest
    {
        public required ConferenceServiceViewModel Service { get; set; }
    }
}
