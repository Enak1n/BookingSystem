using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Entities.Outbox;
using BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces;
using Newtonsoft.Json;

namespace BookingSystem.BL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageRepository _messageRepository;

        public PaymentService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
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

            await _messageRepository.AddAsync(message);
            await _messageRepository.UnitOfWork.SaveChangesAsync();      
        }
    }
}
