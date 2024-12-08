using BookingSystem.Domain.Exceptions;
using System.Numerics;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public class Ticket : BaseModel
    {
        public string FlightNumber { get; private set; }

        public Plane Plane { get; private set; }

        private Ticket(Guid id, string flightNumber, Plane plane)
        {
            Id = id;
            FlightNumber = flightNumber;
            Plane = plane;
        }

        public static Ticket Create(Guid id, string flightNumber, Plane plane)
        {
            if (string.IsNullOrEmpty(flightNumber))
                throw new DomainException("Номер билета не может быть пустым!");

            return new Ticket(id, flightNumber, plane);
        } 
    }
}
