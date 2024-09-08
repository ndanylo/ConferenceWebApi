using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.ValueObjects.ConferenceHall
{
    public class RentPrice : ValueObject
    {
        public decimal Value { get; }

        private RentPrice(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Rent price must be greater than zero.", nameof(value));
            }

            Value = value;
        }

        public static RentPrice Default => new RentPrice(1);

        public static Result<RentPrice> Create(decimal value)
        {
            if (value <= 0)
            {
                return Result.Failure<RentPrice>("Rent price must be greater than zero.");
            }

            var rentPrice = new RentPrice(value);
            return Result.Success(rentPrice);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
