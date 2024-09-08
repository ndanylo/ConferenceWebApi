namespace ConferenceHalls.Application.ViewModels
{
    public class ConferenceHallViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal RentPrice { get; set; }
        public List<ConferenceServiceViewModel> Services { get; set; } = new List<ConferenceServiceViewModel>();
    }
}
