﻿using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public static class ProductDb
    {
        /// <summary>
        /// Returns the total count of products
        /// </summary>
        /// <param name="_context">DB context to use</param>
        /// <returns></returns>
        public async static Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await (from p in _context.Products
                          select p).CountAsync();
        }

        /// <summary>
        /// Returns a page worth of products
        /// </summary>
        /// <param name="_context">DB context to use</param>
        /// <param name="pageSize">The num of products per page</param>
        /// <param name="pageNum">The page to return</param>
        /// <returns></returns>
        public async static Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {
            return await (from p in _context.Products
                       orderby p.Title ascending
                       select p)
                       .Skip(pageSize * (pageNum - 1))
                       .Take(pageSize)
                       .ToListAsync();
        }

        public async static Task<Product> AddProductAsync(ProductContext _context, Product p)
        {
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public static async Task<Product> GetProductAsync(ProductContext _context, int id)
        {
            return await (from prod in _context.Products
                   where prod.ProductId == id
                   select prod).SingleAsync();
        }

        public static async Task<Product> DeleteProductAsync(ProductContext _context, Product p)
        {
            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return p;
        }

        public static async Task<Product> EditProductAsync(ProductContext _context, Product p)
        {
            _context.Entry(p).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return p;
        }
    }
}
