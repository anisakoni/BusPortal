using AutoMapper; 
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace BusPortal.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper; 
        private readonly UserService _userService;


        public ClientsController(IClientService clientService, IMapper mapper, UserService userService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _mapper = mapper;
            _userService = userService;
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();  
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(BusPortal.Web.Models.DTO.RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(viewModel.Email, viewModel.Password, viewModel.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Privacy", "Home");
                }
                return View(viewModel);
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _userService.LoginUserAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe);

            if (result.Succeeded)
            {
                return RedirectToAction("Privacy", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "This account has been locked out.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
