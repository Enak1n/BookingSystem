﻿using Confluent.Kafka;

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
                Acks = Acks.All,
 
            };

            _consumerConfig = new ConsumerConfig
            {
                GroupId = $"custom-group PaymentService",
                BootstrapServers = host,
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest  
            };

            _producer = new ProducerBuilder<int, string>(_producerConfig)
                .Build();

            _consumer = new ConsumerBuilder<int, string>(_consumerConfig).Build();

            _consumer.Subscribe("test");
        }

        public async Task SendMessage(string topic, string message)
        {
            var newMessage = new Message<int, string>
            {
                Key = 1,
                Value = message,
                Headers = null
            };

            await _producer.ProduceAsync(topic, newMessage);
        }

        public async Task<string?> ConsumeMessage(string topic)
        {
            var messageFetchedFromTopic = _consumer.Consume(TimeSpan.FromSeconds(10));

            if (messageFetchedFromTopic == null)
                return null;

            string message = messageFetchedFromTopic.Message.Value;

            _consumer.Commit(messageFetchedFromTopic);

            return messageFetchedFromTopic.Message.Value;
        }

        public void Dispose()
        {
            _producer?.Dispose();
            _consumer?.Dispose();
        }
    }
}