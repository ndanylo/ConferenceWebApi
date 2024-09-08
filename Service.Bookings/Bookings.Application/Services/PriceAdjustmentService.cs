using Bookings.Application.Services.Abstractions;

namespace Bookings.Application.Services
{
    public class PriceAdjustmentService : IPriceAdjustmentService
    {
        public decimal AdjustPrice(decimal basePrice, DateTime startDateTime, TimeSpan duration)
        {
            var startTime = startDateTime.TimeOfDay;
            var endTime = startTime + duration;

            decimal adjustedPrice = basePrice;
            
            if (startTime >= TimeSpan.FromHours(18) && endTime <= TimeSpan.FromHours(23)) 
            {
                adjustedPrice *= 0.80m; // 20% discount
            }
            else if (startTime >= TimeSpan.FromHours(6) && endTime <= TimeSpan.FromHours(9))
            {
                adjustedPrice *= 0.90m; // 10% discount
            }
            else if (startTime >= TimeSpan.FromHours(12) && endTime <= TimeSpan.FromHours(14))
            {
                adjustedPrice *= 1.15m; // 15% increase
            }

            return adjustedPrice;
        }
    }
}