using MassTransit;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;
using MediatR;
using Bookings.Application.Commands;

namespace Bookings.Application.Consumers
{
    public class DeleteBookingsByConferenceHallConsumer : IConsumer<DeleteBookingsByConferenceHallIdRequest>
    {
        private readonly IMediator _mediator;

        public DeleteBookingsByConferenceHallConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<DeleteBookingsByConferenceHallIdRequest> context)
        {
            var deleteCommand = new DeleteBookingsByConferenceHallIdCommand
            {
                ConferenceHallId = context.Message.ConferenceHallId
            };

            var result = await _mediator.Send(deleteCommand);
            if (result.IsSuccess)
            {
                await context.RespondAsync(new DeleteBookingsByConferenceHallResponse
                {
                    IsSuccess = true
                });
            }
            else
            {
                await context.RespondAsync(new DeleteBookingsByConferenceHallResponse
                {
                    IsSuccess = false,
                    Error = result.Error
                });
            }
        }
    }
}
