namespace BookingSystem.BL.Models
{
    public class FlightFilterParams
    {
        public string? Destination { get; set; } 
        public decimal? MinPrice { get; set; } 
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
