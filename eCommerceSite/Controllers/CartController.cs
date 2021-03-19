using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // Id of the product to add
        {
            // Get from DB

            // Add product to cart cookie

            // redirect back to the same page of catalog

            return View();
        }

        public IActionResult Summary()
        {
            // Display all products in shopping cart cookie

            return View();
        }
    }
}
