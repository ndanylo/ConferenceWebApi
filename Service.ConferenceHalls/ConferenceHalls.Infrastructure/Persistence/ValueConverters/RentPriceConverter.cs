using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConferenceHalls.Infrastructure.Persistence.ValueConverters
{
    public class RentPriceConverter : ValueConverter<RentPrice, decimal>
    {
        public RentPriceConverter() 
            : base(
                rentPrice => rentPrice.Value,
                value => RentPrice.Create(value).Value
            ) { }
    }
}
