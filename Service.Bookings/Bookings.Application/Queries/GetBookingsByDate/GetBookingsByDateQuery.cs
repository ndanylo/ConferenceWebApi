using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Queries
{
    public class GetBookingsByDateQuery : IRequest<Result<IEnumerable<BookingViewModel>>>
    {
        public DateTime Date { get; set; }
    }
}
