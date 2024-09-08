using ConferenceHalls.Domain.ValueObjects;
using ConferenceHalls.Domain.ValueObjects.ConferenceService;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConferenceHalls.Infrastructure.Persistence.ValueConverters
{
    public class ServicePriceConverter : ValueConverter<ServicePrice, decimal>
    {
        public ServicePriceConverter() 
            : base(
                servicePrice => servicePrice.Value,
                value => ServicePrice.Create(value).Value
            ) { }
    }
}
