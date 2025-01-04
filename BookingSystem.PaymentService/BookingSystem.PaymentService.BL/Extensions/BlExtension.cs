using BookingSystem.PaymentService.BL.Services;
using BookingSystem.PaymentService.BL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.PaymentService.BL.Extensions
{
    public static class BlExtension
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
