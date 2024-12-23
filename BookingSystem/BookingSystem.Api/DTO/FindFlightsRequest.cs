namespace BookingSystem.Api.DTO
{
    public record FindFlightsRequest(string DeparturePoint, string DestinationPoint, DateTime DeparturedDate);
}
