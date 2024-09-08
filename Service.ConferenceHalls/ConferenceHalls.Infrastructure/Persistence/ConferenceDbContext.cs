using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConferenceHalls.Infrastructure.Persistence
{
    public class ConferenceDbContext : DbContext
    {
        public DbSet<ConferenceHall> ConferenceHalls { get; set; }
        public DbSet<ConferenceService> ConferenceServices { get; set; }

        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
            : base(options) 
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConferenceHallConfiguration());
            modelBuilder.ApplyConfiguration(new ConferenceServiceConfiguration());
        }
    }
}
