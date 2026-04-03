using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GrocMart.web.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersGroup(this IEndpointRouteBuilder endpoint)
        {
            return endpoint
            .MapGroup("Users");
        }
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            IEndpointRouteBuilder userGroup = UsersEndpoints.MapUsersGroup(endpoint);

            userGroup.MapGet("", GetUsers);
            userGroup.MapPost("", CreateUsersRequest);
            return endpoint;
        }
        private static Ok<IEnumerable<UsersDto>> GetUsers(UsersServices UsersService)
        {
            IEnumerable<UsersDto> Users = UsersService.GetUserslist();
            return TypedResults.Ok(Users);

        }
        private static IResult CreateUsersRequest(CreateUsersRequest request, UsersServices UsersService)
        {
            var result = UsersService.CreateUsersRequest(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to create user");
        }
    }
}
