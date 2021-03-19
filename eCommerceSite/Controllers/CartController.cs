using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Adds a product to the shopping cart
        /// </summary>
        /// <param name="id">The Id of the product to store</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id, string prevUrl) // Id of the product to add
        {
            // Get from DB
            Product p = await ProductDb.GetProductAsync(_context, id);

            CookieHelper.AddProductToCart(_httpContext, p);

            TempData["Message"] = $"{p.Title} added to the cart successfully";

            // redirect back to the same page of catalog
            return Redirect(prevUrl);
        }

        public IActionResult Summary()
        {
            // Display all products in shopping cart cookie

            List<Product> cartProducts = CookieHelper.GetCartProducts(_httpContext);
            return base.View(cartProducts);
        }
    }
}
