using BookingSystem.PaymentService.Api.DTO;
using BookingSystem.PaymentService.Api.Utils;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using BookingSystem.PaymentService.Infrastructure.Entities;
using MessageBus;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _cache;

        public GetRequestForPaymentJob(ILogger<GetRequestForPaymentJob> logger, KafkaMessageBus messageBus,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient, IDistributedCache cache)
        {
            _logger = logger;
            _messageBus = messageBus;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentClient = paymentClient;
            _cache = cache;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messageJson = await _messageBus.ConsumeMessage("test");

            if (messageJson != null)
            {
                var message = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(messageJson);

                var paymentId = await _paymentClient.CreatePaymentAsync(message.Price);

                var paymentStatus = new PaymentStatus
                {
                    Id = Guid.NewGuid(),
                    FlightId = message.FlightId,
                    Status = Status.Pending,
                    PaymentId = paymentId,
                    PaymentEndDate = DateTime.UtcNow.AddMinutes(10)
                };

                await _cache.SetStringAsync(paymentId, messageJson, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                await _paymentStatusRepository.AddAsync(paymentStatus);
                await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
            }
        }
    }
}
