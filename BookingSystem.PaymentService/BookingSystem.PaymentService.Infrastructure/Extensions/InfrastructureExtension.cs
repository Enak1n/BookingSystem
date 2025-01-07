using BookingSystem.PaymentService.Domain.SeedWork;
using BookingSystem.PaymentService.Infrastructure.Data;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using BookingSystem.PaymentService.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.PaymentService.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager config)
        {
            var connectionString = config.GetConnectionString("DbConnection");
            var redisConnection = config.GetSection("Redis");

            services.AddAutoMapper(typeof(DataBaseMappings));

            services.AddDbContext<BookingContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IPaymentStatusRepository, PaymentStatusReopository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection.GetSection("Connection").Value;
                options.InstanceName = "local";
            });

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
