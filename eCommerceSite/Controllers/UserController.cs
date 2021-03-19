﻿using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // check if user is logged in
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //UserAccount account = await (from u in _context.UserAccounts
            //                             where u.Username == model.UsernameOrEmail
            //                                || u.Email == model.UsernameOrEmail
            //                                && u.Password == model.Password
            //                             select u).SingleOrDefaultAsync();

            UserAccount account = await _context.UserAccounts
                        .Where(userAcc => (userAcc.Username == model.UsernameOrEmail ||
                                            userAcc.Email == model.UsernameOrEmail) &&
                                            userAcc.Password == model.Password)
                        .SingleOrDefaultAsync();
            if (account == null)
            {
                // Credentials did not match
                // Error Message
                ModelState.AddModelError(string.Empty, "Credentials were not found");

                return View(model);
            }

            //Log user into website
            HttpContext.Session.SetInt32("UserId", account.UserId);

            return RedirectToAction("Index", "Home");


        }

        public IActionResult Logout()
        {
            // get rid of session data
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
