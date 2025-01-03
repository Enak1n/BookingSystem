namespace BookingSystem.Infrastructure.Entities.Outbox
{
    public class BrokerMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Status Status { get; set; }
    }
}
