namespace BusPortal.DAL.Persistence.Entities
{
    public class Booking : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public required Client Client { get; set; }
        public required Line Line { get; set; }
        public DateTime DateTime { get; set; }
        public required string Seat { get; set; }

    }
}
