using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class CartDto(int Id, int UserID, int ProductID, int Quantity)
    {
        public int Id { get; }= Id;
        public int UserID { get; } = UserID;
        public int ProductID { get; } = ProductID;
        public int Quantity { get; } = Quantity;    

    }
}
