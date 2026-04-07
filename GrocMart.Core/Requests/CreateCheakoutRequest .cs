using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public class CreateCheckoutRequest
    {
        public int UserId { get; set; }
        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
