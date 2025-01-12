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
        private readonly KafkaMessageBus _messageBus;
        private readonly PaymentClient _paymentClient;
        private readonly IDistributedCache _cache;

        public GetRequestForPaymentJob(KafkaMessageBus messageBus,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient, IDistributedCache cache)
        {
            _messageBus = messageBus;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentClient = paymentClient;
            _cache = cache;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = new List<string>();

            Console.WriteLine($"Start {DateTime.UtcNow} Thread {context.FireInstanceId}");
            while (true)
            {
                var consumeResult = await _messageBus.ConsumeMessage("test");

                if (consumeResult == null)
                    break;

                messages.Add(consumeResult);
            }

            var paymentStatuses = new List<PaymentStatus>();

            foreach (var messageJson in messages)
            {
                if (messageJson != null)
                {
                    var message = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(messageJson);

                    var paymentId = await _paymentClient.CreatePaymentAsync(message.Price);

                    paymentStatuses.Add(new PaymentStatus
                    {
                        Id = Guid.NewGuid(),
                        FlightId = message.FlightId,
                        Status = Status.Pending,
                        PaymentId = paymentId,
                        PaymentEndDate = DateTime.UtcNow.AddMinutes(10)
                    });

                    await _cache.SetStringAsync(paymentId, messageJson, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    });

                }
            }

            if (paymentStatuses.Count > 0)
            {
                await _paymentStatusRepository.AddRangeAsync(paymentStatuses);
                await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
            }

            Console.WriteLine($"End {DateTime.UtcNow} MessagesCount = {messages.Count} Thread {context.FireInstanceId}");
        }
    }
}
