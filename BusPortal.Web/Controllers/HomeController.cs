using BusPortal.BLL.Domain.Models;
using BusPortal.Web.Models;
using BusPortal.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using BusPortal.DAL.Persistence.Repositories;

namespace BusPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StripeSettings _stripeSettings;
        private readonly ILineRepository _lineRepository;

        public HomeController(ILogger<HomeController> logger, IOptions<StripeSettings> stripeSettings, ILineRepository lineRepository)
        {
            _logger = logger;
            _stripeSettings = stripeSettings.Value;
            _lineRepository = lineRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetPrice(string startCity, string destinationCity)
        {
            if (string.IsNullOrEmpty(startCity) || string.IsNullOrEmpty(destinationCity))
            {
                return BadRequest("Start city and destination city are required.");
            }

            var price = await _lineRepository.GetPriceByRouteAsync(startCity, destinationCity);
            if (price == null)
            {
                return NotFound("Price not found for the selected route.");
            }

            return Ok(price);
        }

        public IActionResult CreateCheckoutSession(string startCity, string destinationCity, decimal price)
        {
            try
            {
                var currency = "usd"; 
                var successUrl = Url.Action("Success", "Home", null, Request.Scheme);
                var cancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme);

                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = (long)(price * 100), 
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Bus Ticket from {startCity} to {destinationCity}"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl
                };

                var service = new SessionService();
                var session = service.Create(options);

                return Redirect(session.Url);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating Stripe Checkout session: {Message}", ex.Message);
                return RedirectToAction("Error");
            }
        }



        public IActionResult Success()
        {

            return View();
        }

        public IActionResult Cancel()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}








