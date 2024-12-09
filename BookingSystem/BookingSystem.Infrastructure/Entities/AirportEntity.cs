namespace BookingSystem.Infrastructure.Entities
{
    public class AirportEntity : BaseEntity
    {
        public string Name { get; set; }

        public int CreatedYear { get; set; }

        public Guid CountryId { get; set; }

        public CountryEntity Country { get; set; }
    }
}
