using Azure.Core;
using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Services.Services;

public sealed class CartServices
{
    private readonly AppDbContext _Dbcontext;
    public CartServices(AppDbContext Dbcontext)
    {
        _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
    }
    public IEnumerable<CartDto> GetCartlist()
    {
        IReadOnlyList<CartDto> cart = _Dbcontext.Cart.Include(b => b.Products).Select(c => new CartDto(c.Id, c.UserID, c.ProductID, c.Products.Name, c.Products.Price, c.Quantity)).ToList();
        return cart;

    }
    public IEnumerable<CartDto> GetCartByUserID(int userId)
    {
        IReadOnlyList<CartDto> cart = _Dbcontext.Cart.Where(c => c.UserID == userId).Select(c => new CartDto(c.Id, c.UserID, c.ProductID, c.Products.Name, c.Products.Price, c.Quantity)).ToList();
        return cart;
    }
    public CartDto? CreateCartRequest(CreateCartRequest request)
    {
        try
        {
            var product = _Dbcontext.Products.FirstOrDefault(p => p.Id == request.ProductID);

            if (product == null || product.AvabilityQuentity < request.Quantity)
            {
                return null;
            }

            //product.AvabilityQuentity -= request.Quantity;

            var cart = new Persistence.Data.Cart
            {
                UserID = request.UserID,
                ProductID = request.ProductID,
                Quantity = request.Quantity
            };
            _Dbcontext.Cart.Add(cart);
            _Dbcontext.SaveChanges();
            return new CartDto(cart.Id, cart.UserID, cart.ProductID, cart.Products.Name, cart.Products.Price, cart.Quantity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the cart: {ex.Message}");
        }
        return null;
    }
    public CartDto? PatchCartRequest(int Id, PatchCartRequest request)
    {
        {
            try
            {
                Cart? cart = _Dbcontext.Cart
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == Id);
                if (cart is null)
                {
                    throw new Exception("Cart not found");
                }
                cart.Quantity = request.Quantity;
                _Dbcontext.SaveChanges();
                return new CartDto(cart.Id, cart.UserID, cart.ProductID, cart.Products.Name, cart.Products.Price, cart.Quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the cart: {ex.Message}");
            }

            return null;
        }
    }
    public void DeleteCart(int Id)
    {
        Cart? cart = _Dbcontext.Cart.Find(Id);
        if (cart is null)
        {
            throw new InvalidOperationException($"Cart with Id {Id} not found.");
        }
        _Dbcontext.Cart.Remove(cart);
        _Dbcontext.SaveChanges();
    }
    
}
