using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
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
            services.AddDbContext<BookingContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
