using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.ValueObjects.ConferenceService
{
    public class ServiceName : ValueObject
    {
        public string Value { get; }

        private ServiceName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Service name cannot be empty or whitespace.", nameof(value));
            }

            Value = value;
        }

        public static ServiceName Default => new ServiceName("DefaultService");

        public static Result<ServiceName> Create(string value)
        {
            try
            {
                return Result.Success(new ServiceName(value));
            }
            catch (ArgumentException ex)
            {
                return Result.Failure<ServiceName>(ex.Message);
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
