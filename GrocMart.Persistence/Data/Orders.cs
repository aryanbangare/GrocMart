using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Data
{
    public sealed class Orders
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public IList<OrderItems> OrderItems { get; init; } = [];
    }
}
