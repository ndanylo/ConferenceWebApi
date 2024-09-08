using Bookings.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Bookings.Domain.Abstractions
{
    public interface IBookingRepository
    {
        Task<Result> AddAsync(Booking booking);
        Task<Result<IEnumerable<Booking>>> GetByConferenceHallIdAsync(Guid conferenceHallId);
        Task<Result> RemoveByIdAsync(Guid bookingId);
        Task<Result> RemoveByConferenceHallIdAsync(Guid conferenceHallId);
        Task<Result<bool>> IsHallAvailableAsync(
            Guid conferenceHallId,
            DateTime date, 
            TimeSpan startTime, 
            TimeSpan duration
        );
        Task<Result<IEnumerable<Booking>>> GetByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<Booking>>> GetByDateAsync(DateTime date);
        Task<Result<Booking>> GetByIdAsync(Guid bookingId);
        Task<Result<IEnumerable<Booking>>> GetBookingsByServiceIdAsync(Guid serviceId);
        Task<Result> UpdateAsync(Booking booking);
    }
}
