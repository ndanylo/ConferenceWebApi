namespace RabbitMQ.Contracts.Requests
{
    public class GetConferenceHallsByIdsRequest
    {
        public Guid[] Ids { get; set; } = Array.Empty<Guid>();
    }
}
