using BookingSystem.PaymentService.Infrastructure.Entities;

namespace BookingSystem.PaymentService.Api.DTO
{
    public class CreatePaymentMessageDto
    {
        public Guid Id { get; set; }
        public FlightDto Flight { get; set; }
        public PassengerEntity Passenger { get; set; }
    }
}
