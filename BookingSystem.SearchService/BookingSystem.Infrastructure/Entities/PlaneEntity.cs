namespace BookingSystem.Infrastructure.Entities
{
    public class PlaneEntity : BaseEntity
    {
        public string Model { get; set; }

        public int YearOfCreation { get; set; }

        public int PassengersCount { get; set; }

        public ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();
    }
}
