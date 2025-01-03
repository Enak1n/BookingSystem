using BookingSystem.PaymentService.Api.DTO;
using BookingSystem.PaymentService.Api.Utils;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using BookingSystem.PaymentService.Infrastructure.Entities;
using MessageBus;
using Newtonsoft.Json;
using Quartz;

namespace BookingSystem.PaymentService.Api.Jobs
{
    public class GetRequestForPaymentJob : IJob
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly ILogger<GetRequestForPaymentJob> _logger;
        private readonly KafkaMessageBus _messageBus;
        private readonly PaymentClient _paymentClient;

        public GetRequestForPaymentJob(ILogger<GetRequestForPaymentJob> logger, KafkaMessageBus messageBus,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient)
        {
            _logger = logger;
            _messageBus = messageBus;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentClient = paymentClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messageJson = await _messageBus.ConsumeMessage("test");

            if (messageJson != null)
            {
                var message = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(messageJson);

                var paymentId = await _paymentClient.CreatePaymentAsync(1000);

                var paymentStatus = new PaymentStatus
                {
                    Id = Guid.NewGuid(),
                    FlightId = message.Flight.Id,
                    Status = Status.Pending,
                    PaymentId = paymentId,
                    PaymentEndDate = DateTime.UtcNow.AddMinutes(10)
                };

                await _paymentStatusRepository.AddAsync(paymentStatus);
                await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
            }
        }
    }
}
