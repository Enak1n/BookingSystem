using BookingSystem.BL.Filters;
using BookingSystem.BL.Services;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using BookingSystem.SearchService.BL.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace BookingSystem.BL.Extensions
{
    public static class BlExtension
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaneService, PlaneService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<ISearchFlightsService, SearchFlightsService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddTransient<IFlightFilter, DestinationFilter>();
            services.AddTransient<IFlightFilter, SortByFilter>();
            services.AddTransient<IFlightFilter, PriceFilter>();
            services.AddTransient<FlightFilterPipeline>();

            return services;
        }
    }
}
