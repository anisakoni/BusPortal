using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusPortal.DAL.Persistence.Repositories
{
    public interface IBookingRepository : _IBaseRepository<Booking, Guid>
    {
        //Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<bool> IsSeatAvailableAsync(string startCity, string destinationCity,
            TimeSpan departureTime, string seat);
        Task<bool> AddAsync(Booking booking);
        Task<IEnumerable<Booking>> GetBookingsByLineAsync(Guid Id, DateTime dateTime);
        Task<IEnumerable<Booking>> GetAsync();
        Task<IEnumerable<int>> GetOccupiedSeatsAsync(string seat);
    }
    internal class BookingRepository : _BaseRepository<Booking, Guid>, IBookingRepository
    {
        private readonly DALDbContext _context;

        public BookingRepository(DALDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        //{
        //    return await _context.Bookings
        //        .Where(b => b.Client.Id = userId)
        //        .OrderByDescending(b => b.BookingDate)
        //        .ToListAsync();
        //}


     
        public async Task<IEnumerable<Booking>> GetBookingsByLineAsync(Guid Id, DateTime dateTime)
       {
            return await _context.Bookings
               .Where(b => b.Line.Id == Id && b.DateTime == dateTime)
               .ToListAsync();
        }

        
        public async Task<IEnumerable<Booking>> GetAsync()
        {
            return await _context.Bookings.ToListAsync();
        }



        public async Task<bool> IsSeatAvailableAsync(decimal price, string seat)
        {
            return !await _context.Bookings
                .AnyAsync(b =>
                    b.Price == price
                    && b.Seat == seat);
        }

      
        public async Task<IEnumerable<int>> GetOccupiedSeatsAsync(string seat)
        {
            var bookings = await _context.Bookings.ToListAsync();
            var occupiedSeats = bookings.Select(b => int.Parse(b.Seat));
            return occupiedSeats;
        }


        public async Task<bool> AddAsync(Booking booking)
        {
            try
            {
                // Check if seat is available before adding
                if (!await IsSeatAvailableAsync(booking.Price, booking.Seat))
                {
                    return false;
                }

                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> IsSeatAvailableAsync(string startCity, string destinationCity, TimeSpan departureTime, string seat)
        {
            throw new NotImplementedException();
        }
    }
}
