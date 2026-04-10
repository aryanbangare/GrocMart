using DocumentFormat.OpenXml.Spreadsheet;
using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence.Data;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GrocMart.web.Endpoints
{
    public static class OrderItemEndpoint
    {
        public static IEndpointRouteBuilder MapOrderItemEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            var orderItemsGroup = endpoint.MapGroup("OrderItems");
            orderItemsGroup.MapGet("", GetOrderItems);
            orderItemsGroup.MapGet("/{orderId}", GetOrderItemsByOrderID);
            orderItemsGroup.MapPost("/checkout", Checkout);

            return endpoint;
        }
        public static Ok<IEnumerable<OrderItemDto>> GetOrderItems(OrderItemServices OrderItemsService)
        {
            IEnumerable<OrderItemDto> OrderItems = OrderItemsService.GetOrderItemslist();
            return TypedResults.Ok(OrderItems);
        }
        public static Ok<IEnumerable<OrderItemDto>> GetOrderItemsByOrderID(int orderId, OrderItemServices OrderItemsService)
        {
            IEnumerable<OrderItemDto> OrderItems = OrderItemsService.GetOrderItemsByOrderID(orderId);
            return TypedResults.Ok(OrderItems);
        }

        public static IResult Checkout(CreateCheckoutRequest request, OrderItemServices OrderItemServices)
        {
            try
            {
                var result = OrderItemServices.CheckoutAsync(request).GetAwaiter().GetResult();
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
