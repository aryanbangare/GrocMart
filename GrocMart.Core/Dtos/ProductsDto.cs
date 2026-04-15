using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class ProductsDto(int Id, string? Name, string? Brand, decimal Price, decimal DiscountPrice, int AvailabilityQuantity)
    {
        public int Id { get; } = Id;
        public string? Name { get; } = Name;
        public string? Brand { get; } = Brand;
        public decimal Price { get; } = Price;
        public decimal DiscountPrice { get; } = DiscountPrice;
        public int AvailabilityQuantity { get; } = AvailabilityQuantity;
    

    }
}
