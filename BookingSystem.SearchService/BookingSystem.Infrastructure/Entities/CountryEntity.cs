namespace BookingSystem.Infrastructure.Entities
{
    public class CountryEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string Code { get; set; }

        public ICollection<AirportEntity> Airports { get; set; } = new List<AirportEntity>();
    }
}
