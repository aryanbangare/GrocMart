using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class OrderItemDto(int ID, int OrderID, int ProductID, int Quantity)
    {
        public int ID { get; }= ID;
        public int OrderID { get; } = OrderID;
        public int ProductID { get; } = ProductID;
        public int Quantity { get; } = Quantity;    

    }
}
