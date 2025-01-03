using BookingSystem.PaymentService.Domain.TicketAggregate;

namespace BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate
{
    public class Ticket : BaseModel
    {
        public Passenger Passenger { get; private set; }

        private Ticket(Guid id, Passenger passenger)
        {
            Id = id;
            Passenger = passenger;
        }

        public static Ticket Create(Guid id, Passenger passenger)
        {
            return new Ticket(id, passenger);
        }
    }
}
