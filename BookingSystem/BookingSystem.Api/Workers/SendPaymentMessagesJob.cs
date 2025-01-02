using BookingSystem.Api.Utils;
using BookingSystem.Infrastructure.Data;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookingSystem.Api.Workers
{
    public class SendPaymentMessagesJob : IJob
    {
        // TODO: переделать на репозиторий
        private readonly BookingContext _bookingContext;
        private readonly KafkaMessageBus _messageBus;
        private readonly ILogger<SendPaymentMessagesJob> _logger;
        private readonly PaymentClient _paymentClient;

        public SendPaymentMessagesJob(BookingContext bookingContext, KafkaMessageBus messageBus,
            ILogger<SendPaymentMessagesJob> logger, PaymentClient paymentClient)
        {
            _bookingContext = bookingContext;
            _messageBus = messageBus;
            _logger = logger;
            _paymentClient = paymentClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _bookingContext.PaymentStatuses.Where(x => x.Status == Status.Created)
                                                                .ToListAsync(context.CancellationToken);

            foreach (var message in messages) 
            {
                try
                {
                    await _messageBus.SendMessage("test", message.Message);
                    _paymentClient.CreatePayment(1000);
                    message.Status = Status.Pending;
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error while sending message {ex.Message}");
                }
            }

            await _bookingContext.SaveChangesAsync();
        }
    }
}
