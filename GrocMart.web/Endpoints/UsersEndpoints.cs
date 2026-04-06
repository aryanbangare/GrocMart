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
            userGroup.MapPost("register", Register);
            userGroup.MapPost("login", Login);
            userGroup.MapDelete("/{Id}", DeleteUsers);
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
        private static IResult Register(CreateUsersRequest request, UsersServices UsersService)
        {
            var result = UsersService.Register(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to register user");
        }
        private static IResult Login(LoginRequest request, UsersServices UsersService)
        {
            var result = UsersService.Login(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to login user");
        }
        public static IResult DeleteUsers(int Id, UsersServices UsersService)
        {
            try
            {
                UsersService.DeleteUsers(Id);
                return TypedResults.Ok($"User with Id {Id} deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }
    }
}
