using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GrocMart.Services.Services
{
    public sealed class CartServices
    {
        private readonly AppDbContext _dbcontext;

        public CartServices(AppDbContext db)
        {
            _dbcontext = db;
        }


        public IEnumerable<CartDto> GetCartByUserID(int userId)
        {
            return _dbcontext.Cart
                .Where(c => c.UserID == userId)
                .Include(c => c.Products)
                .Select(c => new CartDto(
                    c.Id,
                    c.UserID,
                    c.ProductID,
                    c.Products.Name,
                    c.Products.Price,
                    c.Quantity
                ))
                .ToList();
        }
        public object AddToCart(AddToCartRequest request)
        {
            var product = _dbcontext.Products
                .FirstOrDefault(p => p.Id == request.ProductID);

            if (product == null)
                return new { success = false, message = "Invalid product id" };

            if (product.AvabilityQuentity < request.Quantity)
                return new { success = false, message = "Not enough stock" };

            var existing = _dbcontext.Cart
                .FirstOrDefault(c =>
                    c.UserID == request.UserID &&
                    c.ProductID == request.ProductID);

            if (existing != null)
            {
                existing.Quantity += request.Quantity;
            }
            else
            {
                _dbcontext.Cart.Add(new Cart
                {
                    UserID = request.UserID,
                    ProductID = request.ProductID,
                    Quantity = request.Quantity
                });
            }

            _dbcontext.SaveChanges();

            return new { success = true };
        }
        public CartDto? PatchCartRequest(int id, PatchCartRequest request)
        {
            var cart = _dbcontext.Cart
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (cart == null) return null;

            if (request.Quantity <= 0)
            {
                _dbcontext.Cart.Remove(cart);
                _dbcontext.SaveChanges();
                return null;
            }

            cart.Quantity = request.Quantity;
            _dbcontext.SaveChanges();

            return new CartDto(
                cart.Id,
                cart.UserID,
                cart.ProductID,
                cart.Products.Name,
                cart.Products.Price,
                cart.Quantity
            );
        }
        public bool DeleteCart(int id)
        {
            var cart = _dbcontext.Cart.Find(id);

            if (cart == null) return false;

            _dbcontext.Cart.Remove(cart);
            _dbcontext.SaveChanges();

            return true;
        }
        
    }
}