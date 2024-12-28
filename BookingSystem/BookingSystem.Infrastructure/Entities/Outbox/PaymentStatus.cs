namespace BookingSystem.Infrastructure.Entities.Outbox
{
    public class PaymentStatus
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime PaymentEndDate { get; set; }
        public Status Status { get; set; }
    }
}
