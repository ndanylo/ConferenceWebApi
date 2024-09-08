using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceHalls.Infrastructure.Persistence.Configurations
{
    public class ConferenceServiceConfiguration : IEntityTypeConfiguration<ConferenceService>
    {
        public void Configure(EntityTypeBuilder<ConferenceService> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Price)
                .HasConversion(new ServicePriceConverter());

            builder.Property(s => s.Name)
                .HasConversion(new ServiceNameConverter());
        }
    }
}
