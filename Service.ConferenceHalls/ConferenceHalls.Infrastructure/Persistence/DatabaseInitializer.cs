using Microsoft.Extensions.DependencyInjection;
using ConferenceHalls.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace ConferenceHalls.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ConferenceDbContext>();
                await dbContext.Database.MigrateAsync();

                if (!dbContext.ConferenceHalls.Any())
                {
                    var conferenceHalls = new List<ConferenceHall>
                    {
                        ConferenceHall.Create(
                            Guid.NewGuid(),
                            "Зал A",
                            50,
                            new List<ConferenceService>(),
                            2000
                        ).Value,
                        ConferenceHall.Create(
                            Guid.NewGuid(),
                            "Зал B",
                            100,
                            new List<ConferenceService>(),
                            3500
                        ).Value,
                        ConferenceHall.Create(
                            Guid.NewGuid(),
                            "Зал C",
                            30,
                            new List<ConferenceService>(),
                            1500
                        ).Value
                    };

                    await dbContext.ConferenceHalls.AddRangeAsync(conferenceHalls);
                }

                if (!dbContext.ConferenceServices.Any())
                {
                    var services = new List<ConferenceService>
                    {
                        ConferenceService.Create(
                            "Проєктор",
                            500
                        ).Value,
                        ConferenceService.Create(
                            "Wi-Fi",
                            300m
                        ).Value,
                        ConferenceService.Create(
                            "Звук",
                            700
                        ).Value
                    };

                    await dbContext.ConferenceServices.AddRangeAsync(services);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
