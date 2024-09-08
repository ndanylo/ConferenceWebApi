namespace Bookings.Application.ViewModels
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public ConferenceHallViewModel ConferenceHall { get; set; } = new ConferenceHallViewModel();
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<ConferenceServiceViewModel> SelectedServices { get; set; } = new List<ConferenceServiceViewModel>();
        public decimal TotalPrice { get; set; } 
    }
}
