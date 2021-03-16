using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// gets the data connection string from Context and uses 
        /// it so only have to change once if necessary
        /// </summary>
        private readonly ProductContext _context;
        
        /// <summary>
        /// Constructs using the information from context
        /// </summary>
        /// <param name="context"></param>
        public ProductController(ProductContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Displays a view that lists all products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // get all products from the Db
            // List<Product> products1 = (from p in _context.Products select p).ToList();
            List<Product> products = await _context.Products.ToListAsync();

            // send list of products to view to be displayed
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        // public IActionResult Add(Product p) { }
        // Using async is much more effecient
        public async Task<IActionResult> Add(Product p)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Products.Add(p);
                await _context.SaveChangesAsync();

                //display success message
                TempData["Message"] = $"{p.Title} was added successfully";

                // redirect back to catalog page
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // get product with corresponding id
            Product p =
                await(from prod in _context.Products
                 where prod.ProductId == id
                 select prod).SingleAsync();

            // pass product to view
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                //display success message
                TempData["Message"] = $"{p.Title} was edited successfully";

                return RedirectToAction("Index");
            }

            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product p = await(from prod in _context.Products
                         where prod.ProductId == id
                         select prod).SingleAsync();
            return View(p);
        }

        [HttpPost]
        // Cannot be same name and parameter type
        // So give nickname
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product p = await(from prod in _context.Products
                        where prod.ProductId == id
                        select prod).SingleAsync();

            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.Title} was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}