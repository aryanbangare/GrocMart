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

            await _Dbcontext.SaveChangesAsync();

            return order.Id;
        }
    }
}
