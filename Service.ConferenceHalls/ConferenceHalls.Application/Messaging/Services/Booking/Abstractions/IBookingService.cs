
using ConferenceHalls.Application.ViewModels;
using CSharpFunctionalExtensions;

namespace ConferenseHalls.Application.Messaging.Services
{
    public interface IBookingService
    {
        Task<Result<bool>> DeleteBookingByConferenceHallId(Guid conferenceHallId);
        Task<Result> RemoveServiceFromBookings(ConferenceServiceViewModel service);
    }
}