using Bookings.Domain.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Commands
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var getBookingResult = await _bookingRepository.GetByIdAsync(request.BookingId);
            if(getBookingResult.IsFailure)
            {
                return Result.Failure(getBookingResult.Error);
            }
            var booking = getBookingResult.Value;

            if (booking == null)
            {
                return Result.Failure("Booking wasn`t found");
            }

            if (booking.UserId != request.UserId)
            {
                return Result.Failure("You are not authorized to delete this booking.");
            }

            var removeBookingResult = await _bookingRepository.RemoveByIdAsync(request.BookingId);
            if(removeBookingResult.IsFailure)
            {
                return Result.Failure(removeBookingResult.Error);
            }
            return Result.Success();
        }
    }
}
