using MediatR;
using Bookings.Domain.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bookings.Application.Commands
{
    public class RemoveServiceFromBookingsCommandHandler 
        : IRequestHandler<RemoveServiceFromBookingsCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<RemoveServiceFromBookingsCommandHandler> _logger;

        public RemoveServiceFromBookingsCommandHandler(
            IBookingRepository bookingRepository,
            ILogger<RemoveServiceFromBookingsCommandHandler> logger
        )
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(
            RemoveServiceFromBookingsCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var getBookingsResult = await _bookingRepository.GetBookingsByServiceIdAsync(request.Service.Id);
                if (getBookingsResult.IsFailure)
                {
                    return Result.Failure(getBookingsResult.Error);
                }

                var bookings = getBookingsResult.Value;
                foreach (var booking in bookings)
                {
                    booking.RemoveService(request.Service.Id, request.Service.Price);

                    var updateResult = await _bookingRepository.UpdateAsync(booking);
                    if (updateResult.IsFailure)
                    {
                        return Result.Failure(updateResult.Error);
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
