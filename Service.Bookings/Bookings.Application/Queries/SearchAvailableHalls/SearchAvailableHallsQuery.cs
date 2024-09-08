using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Queries
{
    public class SearchAvailableHallsQuery 
        : IRequest<Result<List<ConferenceHallViewModel>>>
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int Capacity { get; set; }
    }
}
