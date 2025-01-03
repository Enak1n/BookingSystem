namespace BookingSystem.PaymentService.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
