namespace BookingSystem.Api.DTO.Flights
{
    public record FindFlightsRequest(string DeparturePoint, string DestinationPoint, DateTime DeparturedDate);
}
