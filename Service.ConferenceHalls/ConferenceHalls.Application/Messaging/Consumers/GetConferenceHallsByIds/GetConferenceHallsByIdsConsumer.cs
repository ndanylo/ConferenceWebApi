using MassTransit;
using MediatR;
using ConferenceHalls.Application.Queries;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;

namespace ConferenceHalls.Appllication.Messaging.Consumers
{
    public class GetConferenceHallsByIdsConsumer : IConsumer<GetConferenceHallsByIdsRequest>
    {
        private readonly IMediator _mediator;

        public GetConferenceHallsByIdsConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetConferenceHallsByIdsRequest> context)
        {
            var query = new GetConferenceHallsByIdsQuery
            {
                Ids = context.Message.Ids
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                await context.RespondAsync(new GetConferenceHallsByIdsResponse
                {
                    ConferenceHalls = result.Value.ToArray()
                });
            }
            else
            {
                throw new Exception(result.Error);
            }
        }
    }
}
