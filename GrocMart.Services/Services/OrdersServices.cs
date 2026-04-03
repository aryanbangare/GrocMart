using GrocMart.Core.Dtos;
using GrocMart.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Services.Services
{
    public sealed class OrdersServices
    {
        private readonly AppDbContext _Dbcontext;
        public OrdersServices(AppDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
        }
        public IEnumerable<OrdersDto> GetOrderslist()
        {
            IReadOnlyList<OrdersDto> orders = _Dbcontext.Orders.Select(o => new OrdersDto(o.Id, o.UserID, o.OrderDate)).ToList();
            return orders;
        }
        public IEnumerable<OrdersDto> GetOrdersByUserID(int userId)
        {
            IReadOnlyList<OrdersDto> orders = _Dbcontext.Orders.Where(o => o.UserID == userId).Select(o => new OrdersDto(o.Id, o.UserID, o.OrderDate)).ToList();
            return orders;
        }
    }
}
