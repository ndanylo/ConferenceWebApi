namespace RabbitMQ.Contracts.Requests
{
    public class GetConferenceServicesByIdsRequest
    {
        public Guid[] Ids { get; set; } = Array.Empty<Guid>();
    }
}
