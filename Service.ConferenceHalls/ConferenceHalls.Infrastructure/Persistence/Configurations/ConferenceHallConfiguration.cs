using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceHalls.Infrastructure.Persistence.Configurations
{
    public class ConferenceHallConfiguration : IEntityTypeConfiguration<ConferenceHall>
    {
        public void Configure(EntityTypeBuilder<ConferenceHall> builder)
        {
            builder.HasKey(h => h.Id);
            
            builder.Property(h => h.Capacity)
                .HasConversion(new CapacityValueConverter())
                .IsRequired();
            
            builder.Property(h => h.Name)
                .HasConversion(new ConferenceHallNameConverter())
                .HasMaxLength(200);
            
            builder.Property(h => h.RentPrice)
                .HasConversion(new RentPriceConverter());
        }
    }
}
