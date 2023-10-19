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
        public async Task<IActionResult> Create(User user)
        {
            if (user != null)
            {
                var reg = await _user.Create(user);
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
            if (login != null)
            {
                var user = await _user.SignIn(login);
                if (user != false)
                {
                    TempData["success"]= true;
                    return RedirectToAction("Index", "Home");
                }
                ViewData["Flag"] = false;
            }
            return View();
        }
        
    }
}
