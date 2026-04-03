using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using GrocMart.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GrocMart.Services.Services
{
    public sealed class ProductsServices
    {
        private readonly AppDbContext _Dbcontext;
        public ProductsServices(AppDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
        }
        public IEnumerable<ProductsDto> GetProductslist()
        {
            IReadOnlyList<ProductsDto> products = _Dbcontext.Products.Select(p => new ProductsDto(p.Id, p.Name, p.Brand, p.Price, p.DiscountPrice, p.AvabilityQuentity)).ToList();
            return products;
        }
        public IEnumerable<ProductsDto> GetProductsById(int Id)
        {
            IReadOnlyList<ProductsDto> products = _Dbcontext.Products.Where(p => p.Id == Id).Select(p => new ProductsDto(p.Id, p.Name, p.Brand, p.Price, p.DiscountPrice, p.AvabilityQuentity)).ToList();
            return products;
        }
        public ProductsDto? CreateProductRequest(CreateProductRequest request)
        {
            try
            {
                var product = new Persistence.Data.Products
                {
                    Name = request.Name,
                    Brand = request.Brand,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,
                    AvabilityQuentity = request.AvabilityQuentity
                };
                _Dbcontext.Products.Add(product);
                _Dbcontext.SaveChanges();
                return new ProductsDto(product.Id, product.Name, product.Brand, product.Price, product.DiscountPrice, product.AvabilityQuentity);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while creating the product: {ex.Message}");

            }
            return null;
        }
        public void DeleteProducts(int Id)
        {
            Products? products = _Dbcontext.Products.Find(Id);
            if (products is null)
            {
                throw new InvalidOperationException($"Product with Id {Id} not found.");
            }
                _Dbcontext.Products.Remove(products);
                _Dbcontext.SaveChanges();
            
        }
    }
}