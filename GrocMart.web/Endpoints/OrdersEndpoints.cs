using GrocMart.Core.Dtos;
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
    }
}
