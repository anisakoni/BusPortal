using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using BusPortal.Common.Models;
using System;
using BusPortal.BLL.Services.Interfaces;

namespace BusPortal.BLL.Services.Scoped
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ILineRepository _lineRepository;



        public BookingServices(

          IBookingRepository bookingRepository,
           IClientRepository clientRepository,
           ILineRepository lineRepository)
        {
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _lineRepository = lineRepository;
        }
        //implementation of the logic to retrieve available seats 
        public async Task<IEnumerable<int>> GetAvailableSeatAsync(Guid Id, DateTime dateTime)
        {
            var bookings = await _bookingRepository.GetBookingsByLineAsync(Id, dateTime);
            var availableSeats = new List<int>();
            for (int i = 1; i <= 40; i++)
            {
                if (!bookings.Any(b =>int.Parse(b.Seat) == i))
                {
                    availableSeats.Add(i);
                }
            }
            return availableSeats;
        }
         //  var bookings = await _bookingRepository.GetBookingsByLineAsync(Id, dateTime);
         //  var availableSeats = new List<int>();

            //  for (int i = 1; i <= 40; i++)
            //   {



            //    if (!bookings.Any(b => b.Seat == i))

            //   {
            //   availableSeats.Add(i);
            //  }
            //   }
            //    return availableSeats;
            //  }

          
          public async Task<IEnumerable<int>> GetOccupiedSeatsAsync()
         {

            var bookings = await _bookingRepository.GetAsync();
             var occupiedSeats = bookings.Select(b =>int.Parse(b.Seat));
              return occupiedSeats;

         }


        //add seat selection system
        public (bool Success, string? ErrorMessage) AddBooking(AddBookingViewModel viewModel, string userName, string seat)
        {
            try
            {
                var client = _clientRepository.GetAll().FirstOrDefault(c => c.Name == userName);
                if (client == null)

                {
                    return (false, "Client not found for the logged-in user.");


                }
                var line = _lineRepository.GetAll().FirstOrDefault();
                if (line == null)
                {
                    return (false, "The specified route does not exist.");

                }
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    Line = line,
                    DateTime = viewModel.DateTime,
                    Seat = viewModel.Seat,
                    Price = viewModel.Price,
                };
                _bookingRepository.Add(booking);
                return (true, null);

            }
            catch (Exception ex)
            {
                //Log the exception as needed
                return (false, "An unexpected error occurred while creating the booking.");
            }



        }
        
     






        //  public (bool Success, string? ErrorMessage) AddBooking(AddBookingViewModel viewModel, string userName)
        //   {
        //     try
        //     {
        //        var client = _clientRepository.GetAll().FirstOrDefault(c => c.Name == userName);
        //        if (client == null)
        //        {
        //            return (false, "Client not found for the logged-in user.");
        //          }

        //     var line = _lineRepository.GetAll().FirstOrDefault();
        //     if (line == null)
        //     {
        //          return (false, "The specified route does not exist.");
        //      }

        //       var booking = new Booking
        //       {
        //          Id = Guid.NewGuid(),
        //         Client = client,
        //         Line = line,
        //         DateTime = viewModel.DateTime,
        //       Seat = viewModel.Seat,
        //       Price = viewModel.Price,

        //    };

        //     _bookingRepository.Add(booking);
        //    return (true, null);
        // }
        //   catch (Exception ex)
        //   {
        // Log the exception as needed
        //      return (false, "An unexpected error occurred while creating the booking.");
        //  }
        // }
        // }
    }
}
