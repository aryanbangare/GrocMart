using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public class CreateProductRequest
    {
        public  string Name { get; set; }
        public  string Brand { get; set; }
        public  decimal Price { get; set; }
        public  decimal DiscountPrice { get; set; }
        public  int AvailabilityQuantity { get; set; } 

    }
}
