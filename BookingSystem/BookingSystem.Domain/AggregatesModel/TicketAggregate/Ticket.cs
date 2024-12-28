using BookingSystem.Domain.Exceptions;
using System.Numerics;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public class Ticket : BaseModel
    {
        public Plane Plane { get; private set; }

        public Passenger Passenger { get; private set; }
        public Flight Fligth { get; private set; }

        private Ticket(Guid id, Plane plane, Passenger passenger, Flight fligth)
        {
            Id = id;
            Plane = plane;
            Passenger = passenger;
            Fligth = fligth;
        }

        public static Ticket Create(Guid id, Plane plane, Passenger passenger, Flight flight)
        {
            return new Ticket(id, plane, passenger, flight);
        }

        public void TryToBuyTicket(Flight flight)
        {
            if (flight.EmptyPlaces <= 0)
                throw new DomainException("Места на данный рейс закончены!");

            flight.TakeASeat();
        }
    }
}
