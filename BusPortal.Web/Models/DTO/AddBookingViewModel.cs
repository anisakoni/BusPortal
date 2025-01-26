namespace BusPortal.Web.Models.DTO
{
    public class AddBookingViewModel
    {
        public required string StartCity { get; set; }
        public required string DestinationCity { get; set; }
        public DateTime DateTime { get; set; }
        //public TimeSpan Time { get; set; }
        public required string Seat { get; set; }

        public decimal Price { get; set; }
    }
}
