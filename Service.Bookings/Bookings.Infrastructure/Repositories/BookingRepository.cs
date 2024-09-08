using Bookings.Domain.Abstractions;
using Bookings.Domain.Entities;
using CSharpFunctionalExtensions;
using MongoDB.Driver;

namespace Bookings.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingRepository(IMongoDatabase database)
        {
            _bookings = database.GetCollection<Booking>("Bookings");
        }

        public async Task<Result<IEnumerable<Booking>>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                var bookings = await _bookings.Find(b => b.UserId == userId).ToListAsync();
                return Result.Success(bookings.AsEnumerable());
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<Booking>>(ex.Message);
            }
        }

        public async Task<Result<bool>> IsHallAvailableAsync(
            Guid conferenceHallId,
            DateTime date, 
            TimeSpan startTime, 
            TimeSpan duration
        )
        {
            try
            {
                var bookings = await _bookings.Find(b =>
                    b.ConferenceHallId == conferenceHallId &&
                    b.Date.Year == date.Year &&
                    b.Date.Month == date.Month &&
                    b.Date.Day == date.Day
                ).ToListAsync();

                var endTime = startTime + duration;
                var isAvailable = !bookings.Any(b =>
                    b.StartTime < endTime &&
                    startTime < (b.StartTime + b.Duration)
                );

                return Result.Success(isAvailable);
            }
            catch(Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
        }

        public async Task<Result> AddAsync(Booking booking)
        {
            try
            {
                await _bookings.InsertOneAsync(booking);
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Booking>>> GetByConferenceHallIdAsync(Guid conferenceHallId)
        {
            try
            {
                var bookings = await _bookings.Find(b => b.ConferenceHallId == conferenceHallId).ToListAsync();
                return Result.Success(bookings.AsEnumerable());
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<Booking>>(ex.Message);
            }
        }

        public async Task<Result> RemoveByIdAsync(Guid bookingId)
        {
            try
            {
                await _bookings.DeleteOneAsync(b => b.Id == bookingId);
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Booking>>> GetByDateAsync(DateTime date)
        {
            try
            {
                var startOfDay = date.Date;
                var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

                var bookings = await _bookings
                    .Find(b => b.Date >= startOfDay && b.Date <= endOfDay)
                    .ToListAsync();

                return Result.Success(bookings.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Booking>>(ex.Message);
            }
        }

        public async Task<Result<Booking>> GetByIdAsync(Guid bookingId)
        {
            try
            {
                var booking = await _bookings
                    .Find(b => b.Id == bookingId)
                    .FirstOrDefaultAsync();

                if (booking == null)
                {
                    return Result.Failure<Booking>("Booking not found.");
                }

                return Result.Success(booking);
            }
            catch(Exception ex)
            {
                return Result.Failure<Booking>(ex.Message);
            }
        }

        public async Task<Result> RemoveByConferenceHallIdAsync(Guid conferenceHallId)
        {
            try
            {
                await _bookings.DeleteManyAsync(b => b.ConferenceHallId == conferenceHallId);
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Booking>>> GetBookingsByServiceIdAsync(Guid serviceId)
        {
            try
            {
                var bookings = await _bookings
                    .Find(b => b.SelectedServices.Contains(serviceId))
                    .ToListAsync();

                return Result.Success<IEnumerable<Booking>>(bookings);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Booking>>(ex.Message);
            }
        }

        public async Task<Result> UpdateAsync(Booking booking)
        {
            try
            {
                var filter = Builders<Booking>.Filter.Eq(b => b.Id, booking.Id);
                var result = await _bookings.ReplaceOneAsync(filter, booking); 

                if (result.ModifiedCount == 0)
                {
                    return Result.Failure("No document was updated.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
