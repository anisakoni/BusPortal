using BusPortal.Web.Data;
using BusPortal.Web.Models;
using BusPortal.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusPortal.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public BookingsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddBookingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
            var user = User.Identity.Name; 
                if(user == null)
                {
                    TempData["ErrorMessage"] = "User identity not found.";
                    return View();
                }
            var client = dbContext.Clients.FirstOrDefault(c => c.Name == user);
            if (client == null)
            {
                TempData["ErrorMessage"] = "Client not found for the logged-in user.";
                return View();
            }

            var line = dbContext.Lines.FirstOrDefault(l => l.StartCity == viewModel.StartCity && l.DestinationCity == viewModel.DestinationCity);

            if (line != null)
            { 
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    Line = line,
                    DateTime = viewModel.Date.Add(viewModel.Time), 
                    Seat = viewModel.Seat
                };
                    dbContext.Bookings.Add(booking);
                    dbContext.SaveChanges();
                }
                else
                {
                    TempData["ErrorMessage"] = "The specified route does not exist.";
                    return View();
                }

                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction("Add");
              
            }
            return View(viewModel);
        }
    }
}
