using BusPortal.BLL.Services.Scoped;
using Microsoft.AspNetCore.Mvc;
using BusPortal.BLL.Services.Interfaces;

namespace BusPortal.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;

        public BookingsController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BusPortal.Web.Models.DTO.AddBookingViewModel viewModel)

        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                if (userName == null)
                {
                    TempData["ErrorMessage"] = "User identity not found.";
                    return View(viewModel);
                }

                var commonViewModel = new BusPortal.Common.Models.AddBookingViewModel
                {
                    StartCity = viewModel.StartCity,
                    DestinationCity = viewModel.DestinationCity,
                    Date = viewModel.Date,
                    Time = viewModel.Time,
                    Seat = viewModel.Seat
                };

                var result = _bookingServices.AddBooking(commonViewModel, userName);

                if (!result.Success)
                {
                    TempData["ErrorMessage"] = result.ErrorMessage;
                    return View(viewModel);
                }

                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction("Add");
            }

            return View(viewModel);
        }
    }
}
