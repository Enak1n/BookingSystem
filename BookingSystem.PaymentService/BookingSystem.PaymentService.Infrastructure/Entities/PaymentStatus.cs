namespace BookingSystem.PaymentService.Infrastructure.Entities
{
    public class PaymentStatus
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public DateTime PaymentEndDate { get; set; }
        public Status Status { get; set; }
        public string PaymentId { get; set; }
    }
}
