using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;

namespace Bookings.Application.Messaging.Services
{
    public interface IConferenceHallService
    {
        Task<Result<List<ConferenceHallViewModel>>> GetConferenceHallsByIdsAsync(IEnumerable<Guid> ids);
        Task<Result<List<ConferenceServiceViewModel>>> GetConferenceServicesByIdsAsync(IEnumerable<Guid> ids);
        Task<Result<List<ConferenceHallViewModel>>> GetConferenceHallsByCapacityAsync(int capacity);
    }
}