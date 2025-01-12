using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using Confluent.Kafka;
using MessageBus;
using Quartz;

namespace BookingSystem.SearchService.Api.Jobs
{
    public class CancelPaymentJob : IJob
    {
        private readonly KafkaMessageBus _messageBus;
        private readonly IFlightService _flightService;

        public CancelPaymentJob(KafkaMessageBus messageBus, IFlightService flightService)
        {
            _messageBus = messageBus;
            _flightService = flightService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = new List<string>();

            while (true)
            {
                var consumeResult = await _messageBus.ConsumeMessage("cancelPayment");

                if (consumeResult == null)
                    break;

                messages.Add(consumeResult);
            }

            foreach(var message in messages)
            {
                await _flightService.ReturnASeat(Guid.Parse(message));
            }
        }
    }
}
