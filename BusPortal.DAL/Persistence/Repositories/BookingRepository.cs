using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.DAL.Persistence.Entities;

namespace BusPortal.DAL.Persistence.Repositories
{
    public interface IBookingRepository : _IBaseRepository<Booking, Guid>
    {
    }
    internal class BookingRepository : _BaseRepository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(DALDbContext dbContext) : base(dbContext)
        { 
        }
    }
}
