namespace BookingSystem.Domain.AggregatesModel.TicketAggregate.Services
{
    public interface IFlightService
    {
        Task<List<Flight>> FindFlightsAsync(string departurePoint, string destinationPoint, DateTime departureDate);
    }
}
