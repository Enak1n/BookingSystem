using BookingSystem.SearchService.BL.Models;

namespace BookingSystem.BL.Models
{
    public class CreatePaymentDto()
    {
        public Guid Id { get; set; }
        public FlightDto Flight { get; set; }
        public PassengerBrokerDto Passenger { get; set; }
    }
}
