namespace BookingSystem.Infrastructure.Entities
{
    public class FlightEntity : BaseEntity
    {
        public string NumberOfTheFlight { get; set; }
        public string DeparturePoint { get; set; }
        public string DestinationPoint { get; set; }

        public Guid PlaneId { get; set; }
        public Guid? DestinationAirportId { get; set; }
        public Guid? DepartureAirportId { get; set; }

        public int EmptyPlaces { get; set; }
        public int Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public PlaneEntity Plane { get; set; }
        public AirportEntity DestinationAirport { get; set; }
        public AirportEntity DepartureAirport { get; set; }
    }
}
