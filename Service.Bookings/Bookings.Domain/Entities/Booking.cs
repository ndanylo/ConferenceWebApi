using Bookings.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Bookings.Domain.Entities
{
    public class Booking : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid ConferenceHallId { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public List<Guid> SelectedServices { get; private set; }
        public TotalPrice TotalPrice { get; private set; }

        private Booking(
            Guid id,
            Guid userId,
            Guid conferenceHallId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan duration,
            List<Guid> selectedServices,
            TotalPrice totalPrice) : base(id)
        {
            UserId = userId;
            ConferenceHallId = conferenceHallId;
            Date = date;
            StartTime = startTime;
            Duration = duration;
            SelectedServices = selectedServices;
            TotalPrice = totalPrice;
        }

        public Result RemoveService(Guid serviceId, decimal servicePrice)
        {
            if(servicePrice < 0)
            {
                return Result.Failure("Service price must be >= 0");
            }

            var serviceToRemove = SelectedServices.FirstOrDefault(s => s == serviceId);
            if(serviceToRemove == Guid.Empty)
            {
                return Result.Failure("Service wasn`t found in booking");
            }

            SelectedServices.Remove(serviceToRemove);
            TotalPrice.Subtract(servicePrice);
            return Result.Success();
        }

        public static Result<Booking> Create(
            Guid bookingId,
            Guid userId,
            Guid conferenceHallId, 
            DateTime date, 
            TimeSpan startTime, 
            TimeSpan duration, 
            List<Guid> selectedServices, 
            Dictionary<Guid, decimal> servicePrices,
            decimal basePricePerHour)
        {
            if(bookingId == Guid.Empty)
            {
                return Result.Failure<Booking>("Id can not be empty");
            }

            if (duration <= TimeSpan.Zero)
            {
                return Result.Failure<Booking>("Duration must be greater than zero");
            }

            if (basePricePerHour < 0)
            {
                return Result.Failure<Booking>("Total price cannot be negative");
            }

            var totalPriceCreateResult = TotalPrice.Calculate(basePricePerHour, duration, selectedServices, servicePrices);

            var booking = new Booking(
                bookingId,
                userId,
                conferenceHallId,
                date,
                startTime,
                duration,
                selectedServices,
                totalPriceCreateResult
            );

            return Result.Success(booking);
        }
    }
}
