using BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces;
using MessageBus;
using Quartz;

namespace BookingSystem.Api.Jobs
{
    public class SendPaymentMessagesJob : IJob
    {
        private readonly IMessageRepository _messageRepository;
        private readonly KafkaMessageBus _messageBus;
        private readonly ILogger<SendPaymentMessagesJob> _logger;

        public SendPaymentMessagesJob(IMessageRepository messageRepository, KafkaMessageBus messageBus,
            ILogger<SendPaymentMessagesJob> logger)
        {
            _messageRepository = messageRepository;
            _messageBus = messageBus;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _messageRepository.GetByStatusAsync(Status.Created);

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

            await _messageRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
