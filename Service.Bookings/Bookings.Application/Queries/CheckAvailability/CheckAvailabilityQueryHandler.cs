using Bookings.Application.Queries;
using Bookings.Domain.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;
namespace Bookings.Application.Handlers
{
    public class CheckAvailabilityQueryHandler : IRequestHandler<CheckAvailabilityQuery, Result<bool>>
    {
        private readonly IBookingRepository _bookingRepository;

        public CheckAvailabilityQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<bool>> Handle(CheckAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var availabilityResult = await _bookingRepository.IsHallAvailableAsync(
                request.ConferenceHallId,
                request.Date,
                request.StartTime,
                request.Duration
            );

            return availabilityResult;
        }
    }
}
