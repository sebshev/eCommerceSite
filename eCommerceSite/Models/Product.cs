using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    /// <summary>
    /// A sellable product 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique identifier for the product
        /// also the Primary Key
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The name of the product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The retail price in US currency
        /// </summary>
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// Category for the product
        /// EX. Electronics, Clothes ect.
        /// </summary>
        public string Category { get; set; }
    }
}
