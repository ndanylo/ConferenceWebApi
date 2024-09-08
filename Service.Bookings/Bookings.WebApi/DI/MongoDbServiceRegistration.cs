using Bookings.Domain.Abstractions;
using Bookings.Infrastructure.Repositories;
using MongoDB.Driver;

namespace Bookings.WebApi.DI
{
    public static class MongoDbServiceRegistration
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoDb");
                return new MongoClient(connectionString);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
                return client.GetDatabase(databaseName);
            });

            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}
