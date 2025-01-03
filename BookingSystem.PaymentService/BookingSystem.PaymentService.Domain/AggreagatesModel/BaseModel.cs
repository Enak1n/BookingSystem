namespace BookingSystem.PaymentService.Domain.AggregatesModel
{
    public class BaseModel
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedDate { get; protected set; }
    }
}
