namespace BookingSystem.BL.Models
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public string DeparturePoint { get; set; }
        public string DestinationPoint { get; set; }
        public int Price { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
