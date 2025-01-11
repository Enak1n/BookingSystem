using BookingSystem.SearchService.BL.Models;

namespace BookingSystem.BL.Models
{
    public class CreatePaymentMessage()
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public int Price { get; set; }
        public PassengerBrokerDto Passenger { get; set; }
    }
}
