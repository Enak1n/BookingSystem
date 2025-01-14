namespace BookingSystem.Domain.AggregatesModel.TicketAggregate.Services
{
    public interface IFlightService
    {
        Task<List<Flight>> FindFlightsAsync(string departurePoint, string destinationPoint, DateTime departureDate);
        Task<Flight> GetInfoAboutFlightAsync(Guid id);
        Task TakeASeat(Guid flightId);
        Task ReturnASeat(Guid flightId);
        Task UpdateFlightAsync(Flight flight);
    }
}
