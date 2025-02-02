using BusPortal.BLL.Services.Scoped;
using Microsoft.AspNetCore.Mvc;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusPortal.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusPortal.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;
        private readonly ILineRepository _lineRepository;

        public BookingsController(IBookingServices bookingServices, ILineRepository lineRepository)
        {
            _bookingServices = bookingServices;
            _lineRepository = lineRepository;
        }

        // Method to fetch available seats for a specific route or booking
        public async Task<IActionResult> ViewAvailableSeats(string startCity, string destinationCity, DateTime dateTime, string seat)
        {
            var line = await _lineRepository.GetLineByRouteAsync(startCity, destinationCity);
            if (line == null)
            {
                ModelState.AddModelError("", "Selected route is not available");
                return View();
            }

            var availableSeats = await _bookingServices.GetAvailableSeatAsync(line.Id, dateTime);
            return View(availableSeats);
        }

        // Method to load the booking form with available start and destination cities
        public async Task<IActionResult> Add()
        {
            var startCities = await _lineRepository.GetAllStartCitiesAsync();
            ViewBag.StartCities = new SelectList(startCities);
            ViewBag.DestinationCities = new SelectList(new List<string>()); // Empty initially

            var occupiedSeats = await _bookingServices.GetOccupiedSeatsAsync();
            ViewBag.OccupiedSeats = occupiedSeats;

            return View();
        }

        // API to fetch destination cities dynamically based on selected start city
        [HttpGet]
        public async Task<IActionResult> GetDestinationCities(string startCity)
        {

                // Get the destination cities related to the start city
                var destinationCities = _lineRepository.GetDestinationCitiesForStartCityAsync(startCity);
                

                // Return the result as a JSON response
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

                var result = _bookingServices.AddBooking(model, User.Identity.Name, model.Seat);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    return View(model);
                }

                return RedirectToAction("Success");
            }

            // Reload start and destination cities if the form submission fails
            var startCities = await _lineRepository.GetAllStartCitiesAsync();
            ViewBag.StartCities = new SelectList(startCities);

            if (!string.IsNullOrWhiteSpace(model.StartCity))
            {
                var destinationCities = await _lineRepository.GetDestinationCitiesForStartCityAsync(model.StartCity);
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
