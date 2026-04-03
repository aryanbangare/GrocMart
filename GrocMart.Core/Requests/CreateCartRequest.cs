using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public class CreateCartRequest
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }   

    }
}
