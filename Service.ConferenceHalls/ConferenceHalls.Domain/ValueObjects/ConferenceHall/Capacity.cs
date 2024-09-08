using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.ValueObjects.ConferenceHall
{
    public class Capacity : ValueObject
    {
        public int Value { get; }

        private Capacity(int value)
        {
            Value = value;
        }

        public static Capacity Default => new Capacity(1);

        public static Result<Capacity> Create(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero", nameof(value));
            }

            var hallCapacity = new Capacity(value);
            return Result.Success(hallCapacity);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Capacity left, Capacity right)
        {
            return left.Value >= right.Value;
        }

        public static bool operator <=(Capacity left, Capacity right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >(Capacity left, Capacity right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(Capacity left, Capacity right)
        {
            return left.Value < right.Value;
        }
    }
}