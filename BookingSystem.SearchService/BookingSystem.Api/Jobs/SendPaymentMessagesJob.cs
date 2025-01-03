using BookingSystem.Infrastructure.Data;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookingSystem.Api.Jobs
{
    public class SendPaymentMessagesJob : IJob
    {
        // TODO: переделать на репозиторий
        private readonly BookingContext _bookingContext;
        private readonly KafkaMessageBus _messageBus;
        private readonly ILogger<SendPaymentMessagesJob> _logger;

        public SendPaymentMessagesJob(BookingContext bookingContext, KafkaMessageBus messageBus,
            ILogger<SendPaymentMessagesJob> logger)
        {
            _bookingContext = bookingContext;
            _messageBus = messageBus;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _bookingContext.Messages.Where(x => x.Status == Status.Created)
                                                                .ToListAsync(context.CancellationToken);

            foreach (var message in messages) 
            {
                try
                {
                    await _messageBus.SendMessage("test", message.Message);
                    message.Status = Status.Sended;
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
