using Bookings.Domain.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Commands
{
    public class DeleteBookingsByConferenceHallIdCommandHandler : IRequestHandler<DeleteBookingsByConferenceHallIdCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingsByConferenceHallIdCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result> Handle(DeleteBookingsByConferenceHallIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookingRepository.RemoveByConferenceHallIdAsync(request.ConferenceHallId);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            return Result.Success();
        }
    }
}
