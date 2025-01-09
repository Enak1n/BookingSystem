using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Entities.Outbox;
using BookingSystem.SearchService.BL.Models;
using BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookingSystem.BL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IFlightService _flightService;
        private readonly IFlightRepository _flightRepository; 
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IMessageRepository messageRepository, IFlightService flightService,
            ILogger<PaymentService> logger, IFlightRepository flightRepository)
        {
            _messageRepository = messageRepository;
            _flightService = flightService;
            _logger = logger;
            _flightRepository = flightRepository;
        }

        public async Task CreatePayment(Guid flightId, PassengerBrokerDto passenger)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight is null)
                throw new ArgumentNullException("Не удалось найти указанный рейс!");

            var paymentDto = new CreatePaymentMessage
            {
                Id = Guid.NewGuid(),
                FlightId = flight.Id,
                Price = flight.Price,
                Passenger = passenger
            };

            var json = JsonConvert.SerializeObject(paymentDto);

            try
            {
                var message = new BrokerMessage
                {
                    Id = Guid.NewGuid(),
                    Message = json,
                    Status = Status.Created
                };

                await _flightService.TakeASeat(paymentDto.FlightId);
                await _messageRepository.AddAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при бронирование места!");
                await _flightService.ReturnASeat(paymentDto.FlightId);
            }

            await _messageRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
