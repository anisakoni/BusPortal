namespace BusPortal.Web.Models.Entities
{
    public class Line
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
        public decimal Price { get; set; }
        // public DateTime Date { get; set; }


    }
}
