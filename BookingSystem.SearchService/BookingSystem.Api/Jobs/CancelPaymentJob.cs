using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
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

            var groupedMessages = messages
                    .GroupBy(Guid.Parse) 
                    .ToDictionary(g => g.Key, g => g.Count());

            foreach (var (flightId, count) in groupedMessages)
            {
                var flight = await _flightService.GetInfoAboutFlightAsync(flightId);
                for (int i = 0; i < count; i++)
                {
                    flight.ReturnASeat();
                }

                await _flightService.UpdateFlightAsync(flight);
            }
        }
    }
}

