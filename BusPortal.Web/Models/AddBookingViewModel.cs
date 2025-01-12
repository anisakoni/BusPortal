using BusPortal.Web.Models.Entities;

namespace BusPortal.Web.Models
{
    public class AddBookingViewModel
    {
        public required string StartCity { get; set; }
        public required string DestinationCity { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public required string Seat { get; set; }
    }
}
