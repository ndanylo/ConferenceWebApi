using MassTransit;
using MediatR;
using RabbitMQ.Contracts.Requests;
using ConferenceHalls.Application.Queries;
using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using RabbitMQ.Contracts.Responses;

namespace ConferenceHallService.Application.Messaging.Consumers
{
    public class GetConferenceHallsByCapacityConsumer : IConsumer<GetConferenceHallsByCapacityRequest>
    {
        private readonly IMediator _mediator;

        public GetConferenceHallsByCapacityConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetConferenceHallsByCapacityRequest> context)
        {
            var capacityResult = Capacity.Create(context.Message.Capacity);
            if (capacityResult.IsFailure)
            {
                throw new Exception(capacityResult.Error);
            }

            var query = new GetConferenceHallsByCapacityQuery
            {
                Capacity = capacityResult.Value
            };

            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                throw new Exception(result.Error);
            }

            await context.RespondAsync(new GetConferenceHallsByCapacityResponse
            {
                ConferenceHalls = result.Value.ToArray()
            });
        }
    }
}
