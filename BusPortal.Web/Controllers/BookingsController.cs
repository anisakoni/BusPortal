using BusPortal.BLL.Services.Scoped;
using Microsoft.AspNetCore.Mvc;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusPortal.DAL.Persistence.Repositories;

namespace BusPortal.Web.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;
        private readonly ILineRepository _lineRepository;


        public BookingsController(IBookingServices bookingServices, ILineRepository lineRepository)
        {
            _bookingServices = bookingServices;
            _lineRepository = lineRepository;

        }
        //     public async Task<IActionResult> Add()
        //   {
        //     var startCities = await _lineRepository.GetAllStartCitiesAsync();
        //      ViewBag.StartCities = new SelectList(startCities);
        //     return View();
        //  }

        [HttpGet]
        public IActionResult Add()
        {
            // Check if user is authenticated
            //    if (!User.Identity.IsAuthenticated)
            //    {
            // Store the intended destination
            //      return RedirectToAction("Login", "Clients", new { ReturnUrl = "/Bookings/Add" });
            //    }

            // User is authenticated, show the booking form
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDestinationCities(string startCity)
        {
            var destinationCities = await _lineRepository.GetDestinationCitiesForStartCityAsync(startCity);
            return Json(destinationCities);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var line = await _lineRepository.GetLineByRouteAsync(model.StartCity, model.DestinationCity);
                if (line == null)
                {
                    ModelState.AddModelError("", "Selected route is not available");
                    return View(model);
                }

                //model.LineId = line.Id;
                //var result = await _bookingServices.CreateBookingAsync(model);
                //if (result.Success)
                //{
                //return RedirectToAction("Index", "Home");
                //}
                //ModelState.AddModelError("", result.ErrorMessage);
            }

            var startCities = await _lineRepository.GetAllStartCitiesAsync();
            ViewBag.StartCities = new SelectList(startCities);
            return View(model);
        }
    }
}


