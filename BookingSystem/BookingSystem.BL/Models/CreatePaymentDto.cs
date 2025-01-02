using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Infrastructure.Entities;

namespace BookingSystem.BL.Models
{
    public class CreatePaymentDto()
    {
        public Guid Id { get; set; }
        //public FlightEntity Flight { get; set; }
        //public PassengerEntity Passenger { get; set; }
    }
}
