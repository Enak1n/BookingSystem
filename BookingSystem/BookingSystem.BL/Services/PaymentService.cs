using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Entities.Outbox;
using MessageBus;
using Newtonsoft.Json;

namespace BookingSystem.BL.Services
{
    public class PaymentService : IPaymentService
    {
        // TODO: Переделать на репозиторий
        private readonly BookingContext _bookingContext;
        private readonly KafkaMessageBus _messageBus;

        public PaymentService(BookingContext bookingContext, KafkaMessageBus messageBus)
        {
            _bookingContext = bookingContext;
            _messageBus = messageBus;
        }

        public async Task CreatePayment(CreatePaymentDto createPaymentDto)
        {
            var json = JsonConvert.SerializeObject(createPaymentDto);

            var paymentStatus = new PaymentStatus
            {
                Id = Guid.NewGuid(),
                PaymentEndDate = DateTime.UtcNow.AddMinutes(10),
                Status = Status.Created,
                Message = json
            };

            await _bookingContext.PaymentStatuses.AddAsync(paymentStatus);
            await _bookingContext.SaveChangesAsync();

            await _messageBus.SendMessage("test", "json");
        }
    }
}
