namespace RabbitMQ.Contracts.Requests
{
    public class DeleteBookingsByConferenceHallIdRequest
    {
        public Guid ConferenceHallId { get; set; }
    }
}
