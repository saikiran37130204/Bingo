using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using BingoWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BingoWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUser _user;
        public UserController(ILogger<UserController> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Registration registration)
        {
            if (registration!=null)
            {
                var reg = await _user.Create(registration);
                if (reg == true)
                {
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Create");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user =  await _user.SignIn(login);
                if (user != false)
                {
                    TempData["userName"] = login.Username;
                    TempData["success"] = true;
                    return RedirectToAction("Index", "Home");
                }
                ViewData["Flag"] = false;
            }
            return View();
        }

        public IActionResult Logout()
        {
            var logout =  _user.SignOut();
            if(logout)
            {
                return View("Login");
            }
            else
            {
                return View("Error");
            }
        }
       
       
        public async Task<IActionResult> InsertInToCart(int ProductId=0)
        {
            var user = await _user.InsertInToCart(ProductId);
            if(user)
            {
                return View();
            }        
            return View(); 
        }
        public async Task<IActionResult> ProfileDetails(int userId)
        {
            var userDetails = await _user.ProfileDetails(userId);
            return View(userDetails);
        }



    }
}
