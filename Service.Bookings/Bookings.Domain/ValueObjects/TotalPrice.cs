using CSharpFunctionalExtensions;

namespace Bookings.Domain.ValueObjects
{
    public class TotalPrice
    {
        public decimal Value { get; private set; }

        public TotalPrice(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Total price cannot be negative.", nameof(value));
            }
            
            Value = value;
        }

        public static TotalPrice Calculate(
            decimal basePricePerHour,
            TimeSpan duration,
            List<Guid> selectedServiceIds,
            Dictionary<Guid, decimal> servicePrices)
        {
            var totalHours = (decimal)duration.TotalHours;
            var hallPrice = basePricePerHour * totalHours;
            
            decimal servicePrice = 0;

            foreach (var serviceId in selectedServiceIds)
            {
                if (servicePrices.TryGetValue(serviceId, out var price))
                {
                    servicePrice += price;
                }
            }

            var totalPrice = hallPrice + servicePrice;

            return new TotalPrice(totalPrice);
        }

        public Result Subtract(decimal amount)
        {
            var newValue = Value - amount;

            if (newValue < 0)
            {
               return Result.Failure("Total price cannot be negative after subtraction.");
            }

            Value = newValue;
            return Result.Success();
        }

        public static explicit operator decimal(TotalPrice totalPrice)
        {
            return totalPrice.Value;
        }

        public override string ToString()
        {
            return $"{Value:C}";
        }
    }
}
