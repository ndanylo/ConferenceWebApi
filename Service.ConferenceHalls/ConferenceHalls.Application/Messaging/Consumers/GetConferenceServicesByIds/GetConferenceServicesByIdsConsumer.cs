using MassTransit;
using MediatR;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;
using ConferenceHalls.Application.Queries;

namespace ConferenceHalls.Appllication.Messaging.Consumers
{
    public class GetConferenceServicesByIdsConsumer : IConsumer<GetConferenceServicesByIdsRequest>
    {
        private readonly IMediator _mediator;

        public GetConferenceServicesByIdsConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetConferenceServicesByIdsRequest> context)
        {
            var query = new GetServicesByIdsQuery
            {
                ServiceIds = context.Message.Ids
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                await context.RespondAsync(new GetConferenceServicesByIdsResponse
                {
                    ConferenceServices = result.Value.ToArray()
                });
            }
            else
            {
                throw new Exception(result.Error);
            }
        }
    }
}
