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
        (bool Success, string? ErrorMessage) AddBooking(AddBookingViewModel viewModel, string userName);
       
    }
}
