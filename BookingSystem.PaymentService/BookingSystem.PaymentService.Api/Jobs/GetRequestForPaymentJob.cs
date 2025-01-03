using BookingSystem.PaymentService.Api.DTO;
using BookingSystem.PaymentService.Api.Utils;
using BookingSystem.PaymentService.Infrastructure.Data;
using BookingSystem.PaymentService.Infrastructure.Entities;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace BookingSystem.PaymentService.Api.Jobs
{
    public class GetRequestForPaymentJob : IJob
    {
        // TODO: переделать на репозиторий.
        private readonly BookingContext _bookingContext;
        private readonly ILogger<GetRequestForPaymentJob> _logger;
        private readonly KafkaMessageBus _messageBus;
        private readonly PaymentClient _paymentClient;

        public GetRequestForPaymentJob(ILogger<GetRequestForPaymentJob> logger, KafkaMessageBus messageBus, BookingContext bookingContext, PaymentClient paymentClient)
        {
            _logger = logger;
            _messageBus = messageBus;
            _bookingContext = bookingContext;
            _paymentClient = paymentClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messageJson = await _messageBus.ConsumeMessage("test");

            if (messageJson != null)
            {
                var message = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(messageJson);

                var paymentId = await _paymentClient.CreatePaymentAsync(1000);

                var existedStatus = await _bookingContext.PaymentStatuses.FirstOrDefaultAsync(x => x.Id == message.Id);

                if (existedStatus == null)
                {
                    var paymentStatus = new PaymentStatus
                    {
                        Id = message.Id,
                        FlightId = message.Flight.Id,
                        Status = Status.Pending,
                        PaymentId = paymentId,
                        PaymentEndDate = DateTime.UtcNow.AddMinutes(10)
                    };

                    await _bookingContext.AddAsync(paymentStatus);
                    await _bookingContext.SaveChangesAsync();
                }
            }
        }
    }
}
