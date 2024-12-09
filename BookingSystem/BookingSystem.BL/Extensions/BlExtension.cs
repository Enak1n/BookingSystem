using BookingSystem.BL.Services;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.BL.Extensions
{
    public static class BlExtension
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaneService, PlaneService>();
            services.AddScoped<IAirportService, AirportService>();

            return services;
        }
    }
}
