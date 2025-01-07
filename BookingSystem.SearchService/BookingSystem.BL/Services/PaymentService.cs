using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using BookingSystem.Infrastructure.Entities.Outbox;
using BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookingSystem.BL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IFlightService _flightService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IMessageRepository messageRepository, IFlightService flightService, ILogger<PaymentService> logger)
        {
            _messageRepository = messageRepository;
            _flightService = flightService;
            _logger = logger;
        }

        public async Task CreatePayment(CreatePaymentDto createPaymentDto)
        {
            var json = JsonConvert.SerializeObject(createPaymentDto);

            try
            {
                var message = new BrokerMessage
                {
                    Id = Guid.NewGuid(),
                    Message = json,
                    Status = Status.Created
                };

                await _flightService.TakeASeat(createPaymentDto.Flight.Id);
                await _messageRepository.AddAsync(message);
                await _messageRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при бронирование места!");
                await _flightService.ReturnASeat(createPaymentDto.Flight.Id);
            }
     
        }
    }
}
