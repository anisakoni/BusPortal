using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.Common.Models;

namespace BusPortal.BLL.Services.Interfaces
{
    public interface IBookingServices
    {
        //  (bool Success, string? ErrorMessage) AddBooking(AddBookingViewModel viewModel, string userName);

        //add  seat selection system
        (bool Success, string? ErrorMessage) AddBooking(Guid clientId, Guid lineId, string seat, DateTime dateTime, decimal price);

        Task<List<string>> GetOccupiedSeatsAsync(string Id, string dateSelected, string timeSelected);
        Task<IEnumerable<string>> GetAvailableDepartureTimesAsync(string startCity, string destinationCity);





    }
}
