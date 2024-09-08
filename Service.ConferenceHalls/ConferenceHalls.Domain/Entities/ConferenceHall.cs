using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.Entities
{
    public class ConferenceHall : Entity<Guid>
    {
        public ConferenceHallName Name { get; private set; } = ConferenceHallName.Default;
        public Capacity Capacity { get; private set; } = Capacity.Default;
        public List<ConferenceService> Services { get; private set; } = new List<ConferenceService>();
        public RentPrice RentPrice { get; private set; } = RentPrice.Default;

        private ConferenceHall() { }

        private ConferenceHall(
            Guid id,
            ConferenceHallName name,
            Capacity capacity,
            List<ConferenceService> services,
            RentPrice rentPrice
        ) : base(id)
        {
            Name = name;
            Capacity = capacity;
            Services = services;
            RentPrice = rentPrice;
        }

        public static Result<ConferenceHall> Create(
            Guid conferenceHallId,
            string name,
            int capacity,
            List<ConferenceService> services,
            decimal rentPrice
        )
        {
            if(conferenceHallId == Guid.Empty)
            {
                return Result.Failure<ConferenceHall>("Id can not be empty");
            }

            var hallNameResult = ConferenceHallName.Create(name);
            if (hallNameResult.IsFailure)
            {
                return Result.Failure<ConferenceHall>(hallNameResult.Error);
            }
            
            var rentPriceResult = RentPrice.Create(rentPrice);
            if (rentPriceResult.IsFailure)
            {
                return Result.Failure<ConferenceHall>(rentPriceResult.Error);
            }

            var capacityResult = Capacity.Create(capacity);
            if (capacityResult.IsFailure)
            {
                return Result.Failure<ConferenceHall>(capacityResult.Error);
            }

            var conferenceHall = new ConferenceHall(
                conferenceHallId,
                hallNameResult.Value,
                capacityResult.Value,
                services,
                rentPriceResult.Value
            );

            return Result.Success(conferenceHall);
        }

        public Result UpdateDetails(
            string? name,
            int? capacity,
            decimal? rentPrice
        )
        {
            if (name != null)
            {
                var hallNameResult = ConferenceHallName.Create(name);
                if (hallNameResult.IsFailure)
                {
                    return Result.Failure(hallNameResult.Error!);
                }
                Name = hallNameResult.Value;
            }
            
            if(capacity.HasValue)
            {
                var capacityResult = Capacity.Create(capacity.Value);
                if(capacityResult.IsFailure)
                {
                    return Result.Failure(capacityResult.Error);
                }
                Capacity = capacityResult.Value;
            }

            if (rentPrice.HasValue)
            {
                var rentPriceResult = RentPrice.Create(rentPrice.Value);
                if (rentPriceResult.IsFailure)
                {
                    return Result.Failure(rentPriceResult.Error!);
                }
                RentPrice = rentPriceResult.Value;
            }

            return Result.Success();
        }

        public Result UpdateRentPrice(decimal newRentPrice)
        {
            var createRentPriceResult = RentPrice.Create(newRentPrice);
            if(createRentPriceResult.IsFailure)
            {
                return Result.Failure(createRentPriceResult.Error);
            }
            RentPrice = createRentPriceResult.Value;

            return Result.Success();
        }

        public Result AddServices(IEnumerable<ConferenceService> newServices)
        {
            try
            {
                foreach (var service in newServices)
                {
                    if (!Services.Contains(service))
                    {
                        Services.Add(service);
                    }
                }
                
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
