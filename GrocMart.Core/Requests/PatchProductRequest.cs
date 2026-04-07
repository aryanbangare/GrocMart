using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public sealed  class PatchProductRequest
    {
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int AvabilityQuentity { get; set; }

    }
}
