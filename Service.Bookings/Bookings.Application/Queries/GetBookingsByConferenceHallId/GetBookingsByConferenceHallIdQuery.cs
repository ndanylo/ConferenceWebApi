using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Queries
{
    public class GetBookingsByConferenceHallIdQuery 
        : IRequest<Result<IEnumerable<BookingViewModel>>>
    {
        public Guid ConferenceHallId { get; set; }
    }
}
