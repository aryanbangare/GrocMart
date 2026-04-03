using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Services.Services
{
    public sealed class CartServices
    {
        private readonly AppDbContext _Dbcontext;   
        public CartServices(AppDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
        }
        public IEnumerable<CartDto> GetCartlist()
        {
            IReadOnlyList<CartDto> cart = _Dbcontext.Cart.Select(c => new CartDto(c.Id, c.UserID, c.ProductID, c.Quantity)).ToList();
            return cart;
        
        }
        public IEnumerable<CartDto> GetCartByUserID(int userId)
        { 
        IReadOnlyList<CartDto> cart = _Dbcontext.Cart.Where(c => c.UserID == userId).Select(c => new CartDto(c.Id, c.UserID, c.ProductID, c.Quantity)).ToList();
            return cart;
        }
        public CartDto? CreateCartRequest(CreateCartRequest request)
        {
            try
            {
                var cart = new Persistence.Data.Cart
                {
                    UserID = request.UserID,
                    ProductID = request.ProductID,
                    Quantity = request.Quantity
                };
                _Dbcontext.Cart.Add(cart);
                _Dbcontext.SaveChanges();
                return new CartDto(cart.Id, cart.UserID, cart.ProductID, cart.Quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the cart: {ex.Message}");
            }
            return null;
        }
    }
}
