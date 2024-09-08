using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Queries
{
    public class GetBookingsByUserIdQuery : IRequest<Result<IEnumerable<BookingViewModel>>>
    {
        public Guid UserId { get; set; }
    }
}
