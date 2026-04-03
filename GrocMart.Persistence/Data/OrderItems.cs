using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Data
{
    public sealed class OrderItems
    {
        public int ID { get; set; }
        public  int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }


    }
}
