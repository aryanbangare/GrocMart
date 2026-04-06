using DocumentFormat.OpenXml.Bibliography;
using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GrocMart.web.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsGroup(this IEndpointRouteBuilder endpoint)
        {
            return endpoint
            .MapGroup("Products");
        }
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            IEndpointRouteBuilder ProductGroup = ProductsEndpoints.MapProductsGroup(endpoint);

            ProductGroup.MapGet("", GetProducts);
            ProductGroup.MapPost("", CreateProductRequest);
            ProductGroup.MapGet("/{Id}", GetProductsById);
            ProductGroup.MapDelete("/{Id}", DeleteProducts);
          

            return endpoint;
        }
        private static Ok<IEnumerable<ProductsDto>> GetProducts(ProductsServices ProductsService)
        {
            IEnumerable<ProductsDto> Products = ProductsService.GetProductslist();
            return TypedResults.Ok(Products);
        }
        private static IResult CreateProductRequest(CreateProductRequest request, ProductsServices ProductsService)
        {
            var result = ProductsService.CreateProductRequest(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to create product");
        }
        private static IResult GetProductsById(int Id, ProductsServices ProductsService)
        {
            IEnumerable<ProductsDto> Products = ProductsService.GetProductsById(Id);
            return TypedResults.Ok(Products);
        }
        public static IResult DeleteProducts(int Id, ProductsServices ProductsService)
        {
            try
            {
                ProductsService.DeleteProducts(Id);
                return TypedResults.Ok($"Product with Id {Id} deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
            
        }
        
        
    }
}
