using ConferenseHalls.Application.Messaging.Services;
using ConferenceHalls.Application.Commands;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class DeleteConferenceHallCommandHandler : IRequestHandler<DeleteConferenceHallCommand, Result>
    {
        private readonly IConferenceHallRepository _repository;
        private readonly IBookingService _bookingService;
        private readonly IMediator _mediator;

        public DeleteConferenceHallCommandHandler(
            IConferenceHallRepository repository,
            IBookingService bookingService,
            IMediator mediator
        )
        {
            _bookingService = bookingService;
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteConferenceHallCommand request, CancellationToken cancellationToken)
        {
            var getHallResult = await _repository.GetByIdsAsync(new [] {request.ConferenceHallId});
            if (getHallResult.IsFailure)
            {
                return Result.Failure($"Conference hall with id {request.ConferenceHallId} not found");
            }
            var conferenceHall = getHallResult.Value.FirstOrDefault();
            if(conferenceHall == null)
            {
                return Result.Failure("Conference wasn`t found");
            }

            var deleteHallResult = await _repository.DeleteAsync(request.ConferenceHallId);
            if (deleteHallResult.IsFailure)
            {
                return Result.Failure($"Failed to delete conference hall {request.ConferenceHallId}: {deleteHallResult.Error}");
            }

            var deleteBookingsResult = await _bookingService.DeleteBookingByConferenceHallId(request.ConferenceHallId);
            if (deleteBookingsResult.IsFailure)
            {
                var restoreCommand = new RestoreConferenceHallCommand(conferenceHall);
                var restoreResult = await _mediator.Send(restoreCommand, cancellationToken);
                if (restoreResult.IsFailure)
                {
                    return Result.Failure($"Failed to delete bookings and restore conference hall");
                }
                return Result.Failure($"Failed to delete bookings for conference hall");
            }

            return Result.Success();
        }
    }
}