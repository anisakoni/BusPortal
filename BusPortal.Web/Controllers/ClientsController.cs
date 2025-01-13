using BusPortal.Web.Models.DTO;
using BusPortal.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BusPortal.Web.Controllers
{
    public class ClientsController : Controller
    {
    //    private readonly ApplicationDbContext dbContext;
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly SignInManager<IdentityUser> _signInManager;
    //    public ClientsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext)
    //    {
    //        this.dbContext = dbContext;
    //        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    //        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));

    //    }

    //    [HttpGet]
    //    public IActionResult Login()  
    //    { 
    //        return View(); 
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Login(LoginViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, isPersistent: false, lockoutOnFailure: false);
    //            var user = await dbContext.Clients.FirstOrDefaultAsync(u => u.Name == viewModel.Username);
    //            if (result.Succeeded)
    //            {
    //                if(user != null)
    //                {
    //                    if (user.Admin == true)
    //                    {
    //                        TempData["SuccessMessage"] = "Login successful!";
    //                        return RedirectToAction("List", "Lines");
    //                    }
    //                    else
    //                    {
    //                        TempData["SuccessMessage"] = "Login successful!";
    //                        return RedirectToAction("Add", "Bookings");
    //                    }
    //                }
    //                else
    //                {
    //                    TempData["ErrorMessage"] = "Client entity not found";
    //                }
                    
                    
    //            }

    //            TempData["ErrorMessage"] = "Invalid login attempt.";
    //        }

    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public IActionResult Register()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = new IdentityUser
    //            {
    //                UserName = viewModel.Name,
    //                Email = viewModel.Email
    //            };

    //            var result = await _userManager.CreateAsync(user, viewModel.Password);

    //            if (result.Succeeded)
    //            {
    //                var newUser = new Client
    //                {
    //                    Id = Guid.NewGuid(),
    //                    Name = viewModel.Name,
    //                    Email = viewModel.Email,
    //                    Admin = false,
    //                };
    //                await dbContext.Clients.AddAsync(newUser);
    //                await dbContext.SaveChangesAsync();

    //                TempData["SuccessMessage"] = "Registration successful!";
    //                return RedirectToAction("Add", "Bookings");
    //            }

    //            foreach (var error in result.Errors)
    //            {
    //                ModelState.AddModelError(string.Empty, error.Description);
    //            }
    //        }

    //        return View(viewModel);
    //    }

    //    public async Task<IActionResult> Logout()
    //    {
    //        await _signInManager.SignOutAsync();
    //        return RedirectToAction("Index", "Home");
    //    }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(string name, string email, string password, string confirmPassword)
        //{
        //    var existingUser = await dbContext.Clients.FirstOrDefaultAsync(u => u.Email == email);
        //    if (existingUser != null)
        //    {
        //        TempData["ErrorMessage"] = "An account with this email already exists.";
        //        return View();
        //    }


        //    var newUser = new Client
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = name,
        //        Email = email,
        //        Admin = false
        //    };


        //    await dbContext.Clients.AddAsync(newUser);

        //    await dbContext.SaveChangesAsync();

        //    return RedirectToAction("Add", "Bookings");
        //}

        

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(string email, string password)
        //{
        //    var user = await dbContext.Clients.FirstOrDefaultAsync(u => u.Email == email);

        //    if (user == null)
        //    {
        //        TempData["ErrorMessage"] = "Invalid email or password.";
        //        return View();
        //    }

        //    if (user.Admin == true)
        //    {
        //        return RedirectToAction("List", "Lines");
        //    }
        //    return RedirectToAction("Add", "Bookings");
        //}


    }
}