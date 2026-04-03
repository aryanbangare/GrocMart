using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class OrdersDto(int Id, int UserID, DateTime OrderDate)
    {
        public int Id { get; } = Id;
        public int UserID { get; } = UserID;    
        public DateTime OrderDate { get; } = OrderDate; 
    }
}
