using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Commands
{
    public class CreateBookingCommand : IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid ConferenceHallId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Guid> SelectedServices { get; set; } = new List<Guid>();
    }
}
