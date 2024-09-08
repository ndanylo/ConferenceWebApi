namespace Bookings.Application.Services.Abstractions
{
    public interface IPriceAdjustmentService
    {
        decimal AdjustPrice(decimal basePrice, DateTime startDateTime, TimeSpan duration);
    }
}