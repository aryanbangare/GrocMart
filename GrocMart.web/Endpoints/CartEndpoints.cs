using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GrocMart.web.Endpoints
{
    public static class CartEndpoints
    {
        public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            var cartGroup = endpoint.MapGroup("Cart");
            cartGroup.MapGet("", GetCart);
            cartGroup.MapGet("/{userId}", GetCartByUserID);
            cartGroup.MapPost("", CreateCartRequest);
            cartGroup.MapPatch("/{Id}", PatchCartRequest);
            cartGroup.MapDelete("/{Id}", DeleteCart);
            return endpoint;
        }

        public static Ok<IEnumerable<CartDto>> GetCart(CartServices CartService)
        {
            IEnumerable<CartDto> Cart = CartService.GetCartlist();
            return TypedResults.Ok(Cart);
        }
        public static Ok<IEnumerable<CartDto>> GetCartByUserID(int userId, CartServices CartService)
        {
            IEnumerable<CartDto> Cart = CartService.GetCartByUserID(userId);
            return TypedResults.Ok(Cart);
        }
        public static IResult CreateCartRequest(CreateCartRequest request, CartServices CartService)
        {
            var result = CartService.CreateCartRequest(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to create cart");
        }
        public static IResult PatchCartRequest(CartServices CartService,int Id, PatchCartRequest request)
        {
            var result = CartService.PatchCartRequest(Id, request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to update cart");
        }
       public static IResult DeleteCart(int Id, CartServices CartService)
        {
            try
            {
                CartService.DeleteCart(Id);
                return TypedResults.Ok($"Cart with Id {Id} deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }
    }
}