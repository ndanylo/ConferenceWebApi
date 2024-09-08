using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConferenceHalls.Infrastructure.Persistence.ValueConverters
{
    public class ConferenceHallNameConverter : ValueConverter<ConferenceHallName, string>
    {
        public ConferenceHallNameConverter() 
            : base(
                hallName => hallName.Value,
                value => ConferenceHallName.Create(value).Value
            ) { }
    }
}
