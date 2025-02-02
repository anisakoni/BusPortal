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
        private readonly ILinesService _linesService;


        public BookingsController(IBookingServices bookingServices, ILineRepository lineRepository, ILinesService linesService)
        {
            _bookingServices = bookingServices;
            _lineRepository = lineRepository;
            _linesService = linesService;

        }



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

     public async Task<IActionResult> Add()
        {
            var startCities = await _lineRepository.GetAllStartCitiesAsync();
            ViewBag.StartCities = new SelectList(startCities);
            ViewBag.DestinationCities = new SelectList(new List<string>()); // Empty initially

            var occupiedSeats = await _bookingServices.GetOccupiedSeatsAsync();
            ViewBag.OccupiedSeats = occupiedSeats;

            return View();
        }

  //      [HttpGet]
   //     public IActionResult Add()
       // {
            // Check if user is authenticated
            //    if (!User.Identity.IsAuthenticated)
            //    {
            // Store the intended destination
            //      return RedirectToAction("Login", "Clients", new { ReturnUrl = "/Bookings/Add" });
            //    }

            // User is authenticated, show the booking form
        //    return View();
      //  }

            //    }

            // User is authenticated, show the booking form
        //    return View();
      //  }

            //    }

            // User is authenticated, show the booking form
        //    return View();
      //  }

        [HttpGet]
        public async Task<IActionResult> GetDestinationCities(string startCity)
        {

                // Get the destination cities related to the start city
                var destinationCities = _lineRepository.GetDestinationCitiesForStartCityAsync(startCity);

            var line = await _lineRepository.GetLineByRouteAsync(model.StartCity, model.DestinationCity);
              if (line == null)
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
