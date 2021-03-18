using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // map data to useraccount object
                UserAccount account = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Username = reg.Username, 
                    Password = reg.Password
                };

                // add to DB
                _context.UserAccounts.Add(account);
                await _context.SaveChangesAsync();
                // redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
