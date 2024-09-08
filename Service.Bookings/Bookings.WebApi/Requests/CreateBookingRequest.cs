namespace Bookings.WebApi.DI
{
    public class CreateBookingRequest
    {
        public Guid ConferenceHallId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Guid> SelectedServices { get; set; } = new List<Guid>();
    }
}