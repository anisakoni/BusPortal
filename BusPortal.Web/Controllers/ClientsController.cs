using AutoMapper; 
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.Web.Models.DTO; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusPortal.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;  

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _mapper = mapper; // Initialize AutoMapper
        }

        // GET Register
        [HttpGet]
        public IActionResult Register()
        {
          
            var registerViewModel = new BusPortal.Common.Models.RegisterViewModel();

           
            var dtoModel = _mapper.Map<BusPortal.Web.Models.DTO.RegisterViewModel>(registerViewModel);

            return View(dtoModel);  
        }

        // POST Register
        [HttpPost]
        public async Task<IActionResult> Register(BusPortal.Web.Models.DTO.RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
                var commonModel = _mapper.Map<BusPortal.Common.Models.RegisterViewModel>(viewModel);

                var result = await _clientService.RegisterClient(commonModel);
                if (result)
                {
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "A client with this email already exists.";
                }
            }

            return View(viewModel); 
        }

        // GET Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST Login
        [HttpPost]
        public async Task<IActionResult> Login(Common.Models.LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isAuthenticated = await _clientService.AuthenticateClient(viewModel);
                if (isAuthenticated)
                {
                    TempData["SuccessMessage"] = "Login successful!";
                  
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                }
            }

            return View(viewModel); 
        }

    
        public async Task<IActionResult> Logout()
        {
            await _clientService.Logout();
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}
