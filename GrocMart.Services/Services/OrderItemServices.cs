using GrocMart.Core.Dtos;
using GrocMart.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Services.Services
{
    public sealed class OrderItemServices
    {
        private readonly AppDbContext _Dbcontext;
        public OrderItemServices(AppDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
        }
        public IEnumerable<OrderItemDto> GetOrderItemslist()
        {
            IReadOnlyList<OrderItemDto> orderItems = _Dbcontext.OrderItems.Select(oi => new OrderItemDto(oi.ID, oi.OrderID, oi.ProductID, oi.Quantity)).ToList();
            return orderItems;
        }
         public IEnumerable<OrderItemDto> GetOrderItemsByOrderID(int orderId)
        {
            IReadOnlyList<OrderItemDto> orderItems = _Dbcontext.OrderItems.Where(oi => oi.OrderID == orderId).Select(oi => new OrderItemDto(oi.ID, oi.OrderID, oi.ProductID, oi.Quantity)).ToList();
            return orderItems;
        }
         
    }
}
