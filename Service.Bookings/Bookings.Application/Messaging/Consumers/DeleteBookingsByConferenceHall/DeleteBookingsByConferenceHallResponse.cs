namespace RabbitMQ.Contracts.Responses
{
    public class DeleteBookingsByConferenceHallResponse
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
    }
}
