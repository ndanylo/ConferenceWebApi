using ConferenceHalls.Domain.ValueObjects.ConferenceService;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.Entities
{
    public class ConferenceService : Entity<Guid>
    {
        public ServiceName Name { get; private set; } = ServiceName.Default;
        public ServicePrice Price { get; private set; } = ServicePrice.Default;

        public ConferenceService() {  }
        private ConferenceService(
            Guid id,
            ServiceName name,
            ServicePrice price
        )
        : base(id)
        {
            Name = name;
            Price = price;
        }

        public static Result<ConferenceService> Create(string name, decimal price)
        {
            var nameResult = ServiceName.Create(name);
            var priceResult = ServicePrice.Create(price);

            if (nameResult.IsFailure)
            {
                return Result.Failure<ConferenceService>(nameResult.Error!);
            }

            if (priceResult.IsFailure)
            {
                return Result.Failure<ConferenceService>(priceResult.Error!);
            }

            var serviceName = nameResult.Value!;
            var servicePrice = priceResult.Value!;

            var conferenceService = new ConferenceService(Guid.NewGuid(), serviceName, servicePrice);

            return Result.Success(conferenceService);
        }
    }
}
