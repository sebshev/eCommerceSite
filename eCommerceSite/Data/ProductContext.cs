using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class ProductContext : DbContext 
    {
        public ProductContext(DbContextOptions<ProductContext> options) 
            :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<eCommerceSite.Models.RegisterViewModel> RegisterViewModel { get; set; }

        public DbSet<eCommerceSite.Models.LoginViewModel> LoginViewModel { get; set; }
    }
}
