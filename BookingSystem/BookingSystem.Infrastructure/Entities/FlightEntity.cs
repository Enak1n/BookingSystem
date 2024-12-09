namespace BookingSystem.Infrastructure.Entities
{
    public class FlightEntity : BaseEntity
    {
        public string DeparturePoint { get; set; }

        public string DestinationPoint { get; set; }

        public Guid PlaneId { get; set; }

        public int EmptyPlaces { get; set; }

        public PlaneEntity Plane { get; set; }
    }
}
