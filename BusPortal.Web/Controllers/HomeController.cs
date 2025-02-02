using BusPortal.BLL.Domain.Models;
using BusPortal.Web.Models;
using BusPortal.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace BusPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StripeSettings _stripeSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<StripeSettings> stripeSettings)
        {
            _logger = logger;
            _stripeSettings = stripeSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateCheckoutSession()
        {
            try
            {
                
                var amount = 1000; 
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
                                UnitAmount = amount, 
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Bus Ticket"
                                }
                            },
                            Quantity = 1 
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl
                };

                // Create Stripe session
                var service = new SessionService();
                var session = service.Create(options);

                // Redirect to Stripe checkout URL
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




