using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Data.Repositories;
using BookingSystem.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager config)
        {
            var connectionString = config.GetConnectionString("DbConnection");

            services.AddAutoMapper(typeof(DataBaseMappings));

            services.AddScoped<IPlaneRepository, PlaneRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();

            services.AddDbContext<BookingContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        /// <summary>
        /// Применение миграций для контекста к базе данных.
        /// </summary>
        public static void DatabaseMigrate(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BookingContext>();

            if (db.Database.IsRelational())
                db.Database.Migrate();
        }

    }
}
