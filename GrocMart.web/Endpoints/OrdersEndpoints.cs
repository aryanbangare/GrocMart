using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GrocMart.web.Endpoints
{
    public static class OrdersEndpoints
    {
        public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            var ordersGroup = endpoint.MapGroup("Orders");
            ordersGroup.MapGet("", GetOrders);
            ordersGroup.MapGet("/{userId}", GetOrdersByUserID);
            ordersGroup.MapPost("checkout", Checkout);
            return endpoint;
        }

        public static Ok<IEnumerable<OrdersDto>> GetOrders(OrdersServices OrdersService)
        {
            IEnumerable<OrdersDto> Orders = OrdersService.GetOrderslist();
            return TypedResults.Ok(Orders);
        }
        public static Ok<IEnumerable<OrdersDto>> GetOrdersByUserID(int userId, OrdersServices OrdersService)
        {
            IEnumerable<OrdersDto> Orders = OrdersService.GetOrdersByUserID(userId);
            return TypedResults.Ok(Orders);
        }
        public static IResult Checkout(CreateCheckoutRequest request, OrdersServices OrdersService)
        {
            try
            {
                var result = OrdersService.CheckoutAsync(request).GetAwaiter().GetResult();
                return result > 0
                    ? TypedResults.Ok($"Order with Id {result} created successfully.")
                    : TypedResults.BadRequest("Failed to create order");
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }
    }
}
