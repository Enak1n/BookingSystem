using Confluent.Kafka;

namespace MessageBus
{
    public sealed class KafkaMessageBus : IDisposable
    {
        private readonly IProducer<int, string> _producer;
        private IConsumer<int, string> _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        public KafkaMessageBus(string host)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = host,
                Acks = Acks.All
            };

            _consumerConfig = new ConsumerConfig
            {
                GroupId = "custom-group",
                BootstrapServers = host,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _producer = new ProducerBuilder<int, string>(_producerConfig)
                .Build();

            _consumer = new ConsumerBuilder<int, string>(_consumerConfig).Build();
        }

        public async Task SendMessage(string topic, string message)
        {
            var random = new Random();
            var newMessage = new Message<int, string>
            {
                Key = random.Next(0, 5),
                Value = message,
                Headers = null
            };

            await _producer.ProduceAsync(topic, newMessage);
        }

        public async Task<string?> ConsumeMessage(string topic)
        {
            _consumer.Subscribe(topic);
            await Task.Yield();
            var messageFetchedFromTopic = _consumer.Consume(TimeSpan.FromSeconds(1));

            if (messageFetchedFromTopic == null)
                return null;

            _consumer.Commit();

            return messageFetchedFromTopic.Message.Value;
        }

        public void Dispose()
        {
            _producer?.Dispose();
            _consumer?.Dispose();
        }
    }
}