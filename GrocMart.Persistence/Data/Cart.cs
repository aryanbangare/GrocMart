using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Data
{
    public sealed class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }   
        public Products? Products { get;set; }
        
    }
}
