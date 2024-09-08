using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Commands
{
    public class DeleteBookingCommand : IRequest<Result>
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
    }
}
