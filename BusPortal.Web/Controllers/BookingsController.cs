using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusPortal.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;
        private readonly ILinesService _linesService;

        public BookingsController(IBookingServices bookingServices, ILinesService linesService)
        {
            _bookingServices = bookingServices;
            _linesService = linesService;
        }

        public async Task<IActionResult> ViewAvailableSeats(string startCity, string destinationCity, DateTime dateTime, string seat)
        {
            var line = await _linesService.GetLineByRouteAsync(startCity, destinationCity);  // Using _linesService
            if (line == null)
            {
                ModelState.AddModelError("", "Selected route is not available");
                return View();
            }

            var availableSeats = await _bookingServices.GetAvailableSeatAsync(line.Id, dateTime);
            return View(availableSeats);
        }

        public async Task<IActionResult> Add()
        {
            var startCities = await _linesService.GetAllStartCitiesAsync();  // Using _linesService
            ViewBag.StartCities = new SelectList(startCities);

            var occupiedSeats = await _bookingServices.GetOccupiedSeatsAsync();
            ViewBag.OccupiedSeats = occupiedSeats;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDestinationCities(string startCity)
        {
            var destinationCities = await _linesService.GetDestinationCitiesForStartCityAsync(startCity);  // Using _linesService
            return Json(destinationCities);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var line = await _linesService.GetLineByRouteAsync(model.StartCity, model.DestinationCity);  // Using _linesService
                if (line == null)
                {
                    ModelState.AddModelError("", "Selected route is not available");
                    return View(model);
                }

                var result = _bookingServices.AddBooking(model, User.Identity.Name, model.Seat);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    return View(model);
                }

                return RedirectToAction("Success");
            }

            // Reload start and destination cities if the form submission fails
            var startCities = await _linesService.GetAllStartCitiesAsync();  // Using _linesService
            ViewBag.StartCities = new SelectList(startCities);

            if (!string.IsNullOrWhiteSpace(model.StartCity))
            {
                var destinationCities = await _linesService.GetDestinationCitiesForStartCityAsync(model.StartCity);  // Using _linesService
                ViewBag.DestinationCities = new SelectList(destinationCities);
            }
            else
            {
                ViewBag.DestinationCities = new SelectList(new List<string>());
            }

            return View(model);
        }
    }
}
