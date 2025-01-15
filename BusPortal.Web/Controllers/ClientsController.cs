using AutoMapper; 
using BusPortal.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusPortal.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;  

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _mapper = mapper;
        }

        
        [HttpGet]
        public IActionResult Register()
        {
          
            var registerViewModel = new BusPortal.Common.Models.RegisterViewModel();

           
            var dtoModel = _mapper.Map<BusPortal.Web.Models.DTO.RegisterViewModel>(registerViewModel);

            return View(dtoModel);  
        }

        
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

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(BusPortal.Web.Models.DTO.LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var commonModel = _mapper.Map<BusPortal.Common.Models.LoginViewModel>(viewModel);
                var isAuthenticated = await _clientService.AuthenticateClient(commonModel);
                if (isAuthenticated)
                {
                    TempData["SuccessMessage"] = "Login successful!";
                  
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                }
            }

            return View(viewModel); 
        }


        public IActionResult Logout()
        {
            _clientService.Logout();
            return RedirectToAction("Login", "Clients");
        }
    }
}
