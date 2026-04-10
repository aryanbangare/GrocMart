using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public  class AddToCartRequest
    {
         public int UserID { get; set; }
         public int ProductID { get; set; }
         public int Quantity { get; set; }
    }
}
