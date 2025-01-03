namespace BookingSystem.PaymentService.Infrastructure.Entities
{
    public class TicketEntity
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public int Seat { get; set; }
        public PassengerEntity Passenger { get; set; }
    }
}
