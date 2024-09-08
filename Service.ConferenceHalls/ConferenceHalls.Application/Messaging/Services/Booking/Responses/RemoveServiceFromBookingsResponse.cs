namespace RabbitMQ.Contracts.Responses
{
    public class RemoveServiceFromBookingsResponse
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        public static RemoveServiceFromBookingsResponse Success()
        {
            return new RemoveServiceFromBookingsResponse
            {
                IsSuccess = true
            };
        }

        public static RemoveServiceFromBookingsResponse Failure(string error)
        {
            return new RemoveServiceFromBookingsResponse
            {
                IsSuccess = false,
                Error = error
            };
        }
    }
}
