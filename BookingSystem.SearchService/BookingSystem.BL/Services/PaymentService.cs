using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Entities.Outbox;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BookingSystem.BL.Services
{
    public class PaymentService : IPaymentService
    {
        // TODO: Переделать на репозиторий
        private readonly BookingContext _bookingContext;
        private readonly IConfiguration _configuration;

        public PaymentService(BookingContext bookingContext, IConfiguration configuration)
        {
            _bookingContext = bookingContext;
            _configuration = configuration;
        }

        public async Task CreatePayment(CreatePaymentDto createPaymentDto)
        {
            var json = JsonConvert.SerializeObject(createPaymentDto);

            var message = new BrokerMessage
            {
                Id = Guid.NewGuid(),
                Message = json,
                Status = Status.Created
            };

            await _bookingContext.Messages.AddAsync(message);
            await _bookingContext.SaveChangesAsync();      
        }
    }
}
