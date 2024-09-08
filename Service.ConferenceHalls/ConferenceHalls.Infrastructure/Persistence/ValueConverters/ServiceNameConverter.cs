using ConferenceHalls.Domain.ValueObjects.ConferenceService;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConferenceHalls.Infrastructure.Persistence.ValueConverters
{
    public class ServiceNameConverter : ValueConverter<ServiceName, string>
    {
        public ServiceNameConverter() 
            : base(
                serviceName => serviceName.Value, 
                value => ServiceName.Create(value).Value)
        { }
    }
}