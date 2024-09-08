namespace ConferenceHalls.Application.ViewModels
{
    public class ConferenceServiceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
