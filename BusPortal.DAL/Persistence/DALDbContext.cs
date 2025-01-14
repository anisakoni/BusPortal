using Microsoft.EntityFrameworkCore;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BusPortal.DAL.Persistence
{
    public class DALDbContext : IdentityDbContext<ApplicationUser>
    {
        public DALDbContext(DbContextOptions<DALDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
