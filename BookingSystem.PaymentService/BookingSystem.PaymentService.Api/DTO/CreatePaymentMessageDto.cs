using BookingSystem.PaymentService.Infrastructure.Entities;

namespace BookingSystem.PaymentService.Api.DTO
{
    public class CreatePaymentMessageDto
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public int Price { get; set; }
        public PassengerEntity Passenger { get; set; }
    }
}
