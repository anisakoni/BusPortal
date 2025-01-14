using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.Common.Models;

namespace BusPortal.BLL.Services.Scoped
{
    public class BookingServices : IBookingServices
    {
        private readonly DALDbContext _dbContext;

        public BookingServices(DALDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (bool Success, string? ErrorMessage) AddBooking(AddBookingViewModel viewModel, string userName)
        {
            try
            {
                var client = _dbContext.Clients.FirstOrDefault(c => c.Name == userName);
                if (client == null)
                {
                    return (false, "Client not found for the logged-in user.");
                }

                var line = _dbContext.Lines.FirstOrDefault(l => l.StartCity == viewModel.StartCity && l.DestinationCity == viewModel.DestinationCity);
                if (line == null)
                {
                    return (false, "The specified route does not exist.");
                }

                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    Line = line,
                    DateTime = viewModel.Date.Add(viewModel.Time),
                    Seat = viewModel.Seat
                };

                _dbContext.Bookings.Add(booking);
                _dbContext.SaveChanges();

                return (true, null);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return (false, "An unexpected error occurred while creating the booking.");
            }
        }
    }
}
