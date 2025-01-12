namespace BusPortal.DAL.Persistence.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
    }
}
