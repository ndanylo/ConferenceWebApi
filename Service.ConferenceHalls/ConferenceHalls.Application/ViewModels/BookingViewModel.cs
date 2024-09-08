namespace ConferenceHalls.Application.ViewModels
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid ConferenceHallId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<Guid> SelectedServices { get; set; } = new List<Guid>();
        public decimal TotalPrice { get; set; } 
    }
}
