using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.ValueObjects.ConferenceHall
{
    public class ConferenceHallName : ValueObject
    {
        public string Value { get; }

        private ConferenceHallName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));
            }

            Value = value;
        }

        public static ConferenceHallName Default => new ConferenceHallName("Default Name");

        public static Result<ConferenceHallName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<ConferenceHallName>("Name cannot be empty or whitespace.");
            }

            var conferenceHallName = new ConferenceHallName(value);
            return Result.Success(conferenceHallName);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
