using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.ValueObjects.ConferenceService
{
    public class ServicePrice : ValueObject
    {
        public decimal Value { get; }

        private ServicePrice(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Service price must be greater than zero.", nameof(value));
            }

            Value = value;
        }

        public static ServicePrice Default => new ServicePrice(1);

        public static Result<ServicePrice> Create(decimal value)
        {
            try
            {
                return Result.Success(new ServicePrice(value));
            }
            catch (ArgumentException ex)
            {
                return Result.Failure<ServicePrice>(ex.Message);
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
