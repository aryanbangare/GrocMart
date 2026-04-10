using GrocMart.Core.Requests;
using GrocMart.Services.Services;

namespace GrocMart.web.Endpoints
{
    public static class CartEndpoints
    {
        public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/cart");

            group.MapGet("/{userId}", GetCart);
            group.MapPost("", AddToCart);
            group.MapPatch("/{id}", UpdateCart);
            group.MapDelete("/{id}", RemoveCart);
            

            return app;
        }
        public static IResult GetCart(int userId, CartServices service)
        {
            var cart = service.GetCartByUserID(userId);
            return Results.Ok(cart);
        }
        public static IResult AddToCart(AddToCartRequest request, CartServices service)
        {
            var result = service.AddToCart(request);
            return Results.Ok(result);
        }
        public static IResult UpdateCart(int id, PatchCartRequest request, CartServices service)
        {
            var result = service.PatchCartRequest(id, request);

            return result != null
                ? Results.Ok(result)
                : Results.BadRequest("Update failed");
        }
        public static IResult RemoveCart(int id, CartServices service)
        {
            var success = service.DeleteCart(id);

            return success
                ? Results.Ok("Removed")
                : Results.NotFound("Item not found");
        }
        
    }
}