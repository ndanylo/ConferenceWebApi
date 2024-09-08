using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Queries
{
    public class CheckAvailabilityQuery : IRequest<Result<bool>>
    {
        public Guid ConferenceHallId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
