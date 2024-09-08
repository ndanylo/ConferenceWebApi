using Bookings.Application.Commands;
using MassTransit;
using MediatR;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;

namespace Bookings.Application.Messaging.Consumers
{
    public class RemoveServiceFromBookingsConsumer : IConsumer<RemoveServiceFromBookingsRequest>
    {
        private readonly IMediator _mediator;

        public RemoveServiceFromBookingsConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RemoveServiceFromBookingsRequest> context)
        {
            var query = new RemoveServiceFromBookingsCommand
            {
                Service = context.Message.Service
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                await context.RespondAsync(RemoveServiceFromBookingsResponse.Success());
            }
            else
            {
                await context.RespondAsync(RemoveServiceFromBookingsResponse.Failure(result.Error));
            }
        }
    }
}
