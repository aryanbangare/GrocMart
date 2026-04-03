using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Data
{
    public sealed class Products
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int AvabilityQuentity { get; set; }

    }
}
