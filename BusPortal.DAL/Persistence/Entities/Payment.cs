using NuGet.Packaging.Signing;
namespace BusPortal.DAL.Persistence.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int amount { get; set; }
        public Timestamp created_at { get; set; }
    }
}
