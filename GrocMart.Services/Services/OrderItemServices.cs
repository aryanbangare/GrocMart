using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> CheckoutAsync(CreateCheckoutRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new Exception("Cart is empty");

  
            var order = new Orders
            {
                UserID = request.UserId,
                OrderDate = DateTime.Now
            };

            _Dbcontext.Orders.Add(order);
            await _Dbcontext.SaveChangesAsync(); 
            foreach (var item in request.Items)
            {
                var product = await _Dbcontext.Products.FindAsync(item.ProductId);

                if (product == null)
                    throw new Exception($"Product {item.ProductId} not found");

                if (product.AvabilityQuentity < item.Quantity)
                    throw new Exception($"Insufficient stock for product {product.Name}");

            
                product.AvabilityQuentity -= item.Quantity;

            
                _Dbcontext.OrderItems.Add(new OrderItems
                {
                    OrderID = order.Id,
                    ProductID = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            var cartItems = await _Dbcontext.Cart
                .Where(c => c.UserID == request.UserId)
                .ToListAsync();

            _Dbcontext.Cart.RemoveRange(cartItems);

        
            await _Dbcontext.SaveChangesAsync();

            return order.Id;
        }
    }
    
}
