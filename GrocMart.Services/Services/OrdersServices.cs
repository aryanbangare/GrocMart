using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public OrdersDto? CreateOrder(CreateOrderRequest request)
        {
            using var transaction = _Dbcontext.Database.BeginTransaction();
            try
            {
          
                var cartItems = _Dbcontext.Cart.Where(c => c.UserID == request.UserID).ToList();
                if (!cartItems.Any())
                {
                    return null; 
                }

                
                var order = new Orders
                {
                    UserID = request.UserID,
                    OrderDate = DateTime.Now
                };
                _Dbcontext.Orders.Add(order);
                _Dbcontext.SaveChanges();

              
                foreach (var item in cartItems)
                {
                    var product = _Dbcontext.Products.FirstOrDefault(p => p.Id == item.ProductID);
                    if (product == null || product.AvabilityQuentity < item.Quantity)
                    {
                       
                        transaction.Rollback();
                        return null; 
                    }

                  
                    product.AvabilityQuentity -= item.Quantity;

                   
                    var orderItem = new OrderItems
                    {
                        OrderID = order.Id,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity
                    };
                    _Dbcontext.OrderItems.Add(orderItem);
                }

                
                _Dbcontext.Cart.RemoveRange(cartItems);

                _Dbcontext.SaveChanges();
                transaction.Commit();

                return new OrdersDto(order.Id, order.UserID, order.OrderDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the order: {ex.Message}");
                transaction.Rollback();
            }
            return null;
        }
    }
}
