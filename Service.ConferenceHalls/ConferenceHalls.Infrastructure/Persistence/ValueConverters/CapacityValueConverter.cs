using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ConferenceHalls.Domain.ValueObjects.ConferenceHall;

public class CapacityValueConverter : ValueConverter<Capacity, int>
{
    public CapacityValueConverter()
        : base(
            capacity => capacity.Value,
            value => Capacity.Create(value).Value)
    { }
}
